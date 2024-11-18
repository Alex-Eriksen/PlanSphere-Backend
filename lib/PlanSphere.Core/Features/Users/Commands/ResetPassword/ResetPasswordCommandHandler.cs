using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.ResetPassword;

[HandlerType(HandlerType.SystemApi)]
public class ResetPasswordCommandHandler(
    ILogger<ResetPasswordCommandHandler> logger,
    UserManager<ApplicationUser> userManager,
    IUserRepository userRepository
) : IRequestHandler<ResetPasswordCommand>
{
    private readonly ILogger<ResetPasswordCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Reset password");
        _logger.LogInformation("Resetting password for user with id: [{userId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        
        var identityUser = await _userManager.Users.AsQueryable().SingleOrDefaultAsync(x => x.Id == user.IdentityUserId, cancellationToken);
        if (identityUser == null)
        {
            _logger.LogInformation("Couldn't find identity user with id {identityUserId}", user.IdentityUserId);
            throw new KeyNotFoundException($"Couldn't find identity user with id {user.IdentityUserId}");
        }

        var result = await _userManager.ResetPasswordAsync(identityUser, request.ResetPasswordVerificationToken, request.Password);
        if (result.Succeeded)
        {
            await _userManager.ConfirmEmailAsync(identityUser, request.EmailVerificationToken);
        }
        _logger.LogInformation("Reset password for user with id: [{userId}]", request.UserId);
    }
}