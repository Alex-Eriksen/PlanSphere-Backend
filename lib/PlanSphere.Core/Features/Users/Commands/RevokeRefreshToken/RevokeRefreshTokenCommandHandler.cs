using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.RevokeRefreshToken;

[HandlerType(HandlerType.SystemApi)]
public class RevokeRefreshTokenCommandHandler(
    ILogger<RevokeRefreshTokenCommandHandler> logger,
    IUserRepository userRepository
) : IRequestHandler<RevokeRefreshTokenCommand>
{
    private readonly ILogger<RevokeRefreshTokenCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Revoke refresh token.");
        _logger.LogInformation("Revoking refresh token.");
        await _userRepository.RevokeRefreshTokenAsync(request.Token, request.IpAddress, cancellationToken);
        _logger.LogInformation("Revoked refresh token.");
    }
}