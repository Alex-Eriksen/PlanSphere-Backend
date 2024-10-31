using MediatR;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Commands.RefreshToken;

public record RefreshTokenCommand(string Token, string IpAddress) : IRequest<RefreshTokenDTO>;
