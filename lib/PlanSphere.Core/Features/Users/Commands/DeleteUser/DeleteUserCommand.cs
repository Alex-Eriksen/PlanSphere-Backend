using MediatR;

namespace PlanSphere.Core.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(ulong userId) : IRequest;
