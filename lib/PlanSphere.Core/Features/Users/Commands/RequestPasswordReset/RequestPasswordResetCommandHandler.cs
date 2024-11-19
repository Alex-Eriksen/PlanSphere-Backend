using System.Web;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;
using PlanSphere.Core.Utilities.Builder;

namespace PlanSphere.Core.Features.Users.Commands.RequestPasswordReset;

[HandlerType(HandlerType.SystemApi)]
public class RequestPasswordResetCommandHandler(
    ILogger<RequestPasswordResetCommandHandler> logger,
    UserManager<ApplicationUser> userManager,
    IUserRepository userRepository,
    IEmailService emailService
) : IRequestHandler<RequestPasswordResetCommand>
{
    private readonly ILogger<RequestPasswordResetCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IEmailService _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    
    public async Task Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Request password reset");
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        var identityUser = await _userManager.Users.AsQueryable().SingleOrDefaultAsync(x => x.Id == user.IdentityUserId, cancellationToken);
        if (identityUser == null)
        {
            _logger.LogInformation("Couldn't find identity user with id {identityUserId}", user.IdentityUserId);
            throw new KeyNotFoundException($"Couldn't find identity user with id {user.IdentityUserId}");
        }

        var emailVerificationToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
        var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
        var encodedTokenEmailVerification = HttpUtility.UrlEncode(emailVerificationToken);
        var encodedResetPasswordToken = HttpUtility.UrlEncode(resetPasswordToken);
        
        var resetPasswordUrl = new EndpointBuilder(Environment.GetEnvironmentVariable(EnvironmentConstants.ApplicationUrl))
            .AddEndpoint("/reset-password")
            .AddQueryParameter(QueryParameterNameConstants.EmailVerificationToken, encodedTokenEmailVerification)
            .AddQueryParameter(QueryParameterNameConstants.EmailVerificationIdentifier, user.Id)
            .AddQueryParameter(QueryParameterNameConstants.ResetPasswordToken, encodedResetPasswordToken)
            .Build();
        
        await _emailService.SendEmailAsync(
            identityUser.Email, 
            EmailTemplates.ResetPasswordSubject,
            string.Format(EmailTemplates.ResetPassword, user.FullName, resetPasswordUrl)
        );
    }
}