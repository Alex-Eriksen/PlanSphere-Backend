using MediatR;

namespace PlanSphere.Core.Features.Roles.Commands.AssignRole;

public record AssignRoleCommand(ulong RoleId, ulong UserId) : IRequest;
