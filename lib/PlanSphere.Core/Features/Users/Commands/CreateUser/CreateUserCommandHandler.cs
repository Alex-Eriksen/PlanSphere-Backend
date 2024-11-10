using System.Web;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.CreateUser;

[HandlerType(HandlerType.SystemApi)]
public class CreateUserCommandHandler(
    IUserRepository userRepository,
    UserManager<ApplicationUser> userManager,
    IMapper mapper,
    ILogger<CreateUserCommandHandler> logger
) : IRequestHandler<CreateUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateUserCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Creating a new user on organisation with id: [{organisationId}]", command.OrganisationId);
        
        _logger.LogInformation("Creating user identity");
        var applicationUser = await CreateIdentityUser(command, cancellationToken);
        _logger.LogInformation("Created user identity");
        
        await CreateUser(command, applicationUser.Id, cancellationToken);
        _logger.LogInformation("Created a new user on organisation with id: [{organisationId}]", command.OrganisationId);
    }

    private async Task<ApplicationUser> CreateIdentityUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(command.Request.Email);
        if (existingUser != null)
        {
            if (await _userRepository.IsUserRegisteredAsync(command.Request.Email, cancellationToken))
            {
                throw new InvalidOperationException("Email is already in use.");
            }

            return existingUser; // Avoid if user has yet to register their details
        }
        
        var applicationUser = new ApplicationUser()
        {
            Email = command.Request.Email,
            UserName = command.Request.Email
        };

        var result = command.WithConfirmationEmail
            ? await _userManager.CreateAsync(applicationUser)
            : await _userManager.CreateAsync(applicationUser, "Passw0rd?");
        
        if (!result.Succeeded)
        {
            _logger.LogInformation("Something went wrong while creating a identity user.");
            throw new InvalidOperationException("Something went wrong.");
        }

        if (command.WithConfirmationEmail) // TODO: SEND EMAIL
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            var encodedToken = HttpUtility.UrlEncode(token);
        }

        return applicationUser;

    }

    private async Task CreateUser(CreateUserCommand command, string applicationUserId, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<CreateUserCommand, User>(command);
        newUser.IdentityUserId = applicationUserId;
        await _userRepository.CreateAsync(newUser, cancellationToken);
    }
}