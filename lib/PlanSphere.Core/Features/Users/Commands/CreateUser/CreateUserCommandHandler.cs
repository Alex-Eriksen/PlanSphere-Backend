using System.Web;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;
using PlanSphere.Core.Utilities.Builder;

namespace PlanSphere.Core.Features.Users.Commands.CreateUser;

[HandlerType(HandlerType.SystemApi)]
public class CreateUserCommandHandler(
    IUserRepository userRepository,
    UserManager<ApplicationUser> userManager,
    IMapper mapper,
    ILogger<CreateUserCommandHandler> logger,
    IEmailService emailService,
    IOrganisationRepository organisationRepository,
    ICompanyRepository companyRepository,
    IDepartmentRepository departmentRepository,
    ITeamRepository teamRepository
) : IRequestHandler<CreateUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<CreateUserCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IEmailService _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));


    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Creating a new user on organisation with id: [{organisationId}]", command.OrganisationId);
        
        _logger.LogInformation("Creating user identity");
        var applicationUser = await CreateIdentityUser(command, cancellationToken);
        _logger.LogInformation("Created user identity");

        var workScheduleId = command.SourceLevel switch
        {
            SourceLevel.Organisation => (await _organisationRepository.GetByIdAsync(command.SourceLevelId, cancellationToken)).Settings.DefaultWorkScheduleId,
            SourceLevel.Company => (await _companyRepository.GetByIdAsync(command.SourceLevelId, cancellationToken)).Settings.DefaultWorkScheduleId,
            SourceLevel.Department => (await _departmentRepository.GetByIdAsync(command.SourceLevelId, cancellationToken)).Settings.DefaultWorkScheduleId,
            SourceLevel.Team => (await _teamRepository.GetByIdAsync(command.SourceLevelId, cancellationToken)).Settings.DefaultWorkScheduleId,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var user = await CreateUser(command, applicationUser.Id, workScheduleId, cancellationToken);
        
        if (command.WithConfirmationEmail)
        {
            var emailVerificationToken = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            var encodedTokenEmailVerification = HttpUtility.UrlEncode(emailVerificationToken);
            var encodedResetPasswordToken = HttpUtility.UrlEncode(resetPasswordToken);
            
            var verificationUrl = new EndpointBuilder(Environment.GetEnvironmentVariable(EnvironmentConstants.ApplicationUrl))
                .AddEndpoint("/reset-password")
                .AddQueryParameter(QueryParameterNameConstants.EmailVerificationToken, encodedTokenEmailVerification)
                .AddQueryParameter(QueryParameterNameConstants.EmailVerificationIdentifier, user.Id)
                .AddQueryParameter(QueryParameterNameConstants.ResetPasswordToken, encodedResetPasswordToken)
                .Build();

            _logger.LogInformation("Sending invitation email to {email}", applicationUser.Email);
            await _emailService.SendEmailAsync(
                applicationUser.Email, 
                EmailTemplates.InvitationSubject,
                string.Format(EmailTemplates.Invitation, user.FullName, verificationUrl)
            );
            _logger.LogInformation("Sent invitation email to {email}", applicationUser.Email);
        }
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
        
        return applicationUser;
    }

    private async Task<User> CreateUser(CreateUserCommand command, string applicationUserId, ulong workScheduleId, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<CreateUserCommand, User>(command);
        newUser.IdentityUserId = applicationUserId;
        newUser.Roles.AddRange(
            command.Request.RoleIds.Select(roleId => new UserRole
            {
                RoleId = roleId,
            })
        );
        
        newUser.JobTitles.AddRange(
            command.Request.JobTitleIds.Select(jobTitleId => new UserJobTitle
            {
                JobTitleId = jobTitleId
            })
        );

        newUser.Settings = new UserSettings()
        {
            WorkSchedule = new WorkSchedule()
            {
                IsDefaultWorkSchedule = false,
                ParentId = workScheduleId
            },
            InheritWorkSchedule = true
        };
        
        return await _userRepository.CreateAsync(newUser, cancellationToken);
    }
}