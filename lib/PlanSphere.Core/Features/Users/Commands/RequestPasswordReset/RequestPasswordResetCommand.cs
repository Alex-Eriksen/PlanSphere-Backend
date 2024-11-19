using MediatR;

namespace PlanSphere.Core.Features.Users.Commands.RequestPasswordReset;

public record RequestPasswordResetCommand(string Email) : IRequest;
