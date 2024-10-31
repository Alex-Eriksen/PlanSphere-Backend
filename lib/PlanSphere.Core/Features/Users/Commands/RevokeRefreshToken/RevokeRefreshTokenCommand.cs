using MediatR;

namespace PlanSphere.Core.Features.Users.Commands.RevokeRefreshToken;

public record RevokeRefreshTokenCommand(string Token, string IpAddress) : IRequest;
