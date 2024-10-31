using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.RefreshToken;

[HandlerType(HandlerType.SystemApi)]
public class RefreshTokenCommandHandler(
    IUserRepository userRepository,
    ILogger<RefreshTokenCommandHandler> logger
) : IRequestHandler<RefreshTokenCommand, RefreshTokenDTO>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<RefreshTokenCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<RefreshTokenDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Refresh access token.");
        _logger.LogInformation("Refreshing access token.");
        var refreshTokenDto = await _userRepository.RefreshTokenAsync(request.Token, request.IpAddress, cancellationToken);
        _logger.LogInformation("Refreshed access token.");

        return refreshTokenDto;
    }
}