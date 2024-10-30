using System.Security.Claims;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.LoginUser;

[HandlerType(HandlerType.SystemApi)]
public class LoginUserCommandHandler(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IUserRepository userRepository,
    IJwtHelper jwtHelper,
    ILogger<LoginUserCommandHandler> logger
) : IRequestHandler<LoginUserCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IJwtHelper _jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));
    private readonly ILogger<LoginUserCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("User is trying to log in!");
        var applicationUser = await Authenticate(request, cancellationToken);
        _logger.LogInformation("User is logged in!");
        
        
        _logger.LogInformation("Fetching user with id: [{userId}]", applicationUser);
        var user = await _userRepository.GetByIdentityId(applicationUser.Id, cancellationToken);
        _logger.LogInformation("Fetched user with id: [{userId}]", applicationUser);
        
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimsConstants.UserId, user.Id.ToString()),
            new(ClaimsConstants.OrganizationId, user.OrganisationId.ToString()),
            new(ClaimsConstants.Email, user.Email),
            new(ClaimsConstants.FirstName, user.FirstName),
            new(ClaimsConstants.LastName, user.LastName)
        };
        
        var token = _jwtHelper.GenerateToken(claims, DateTime.UtcNow.AddDays(1));

        return token;
    }

    private async Task<ApplicationUser> Authenticate(LoginUserCommand userCommand, CancellationToken cancellationToken)
    {
        var applicationUser = await _userManager.FindByEmailAsync(userCommand.Email);
        if (applicationUser == null)
        {
            _logger.LogInformation("Invalid email or password!");
            throw new KeyNotFoundException("Invalid email or password!");
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(applicationUser, userCommand.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            _logger.LogInformation("Invalid email or password!");
            throw new KeyNotFoundException("Invalid email or password!");
        }
        return applicationUser;
    }
}