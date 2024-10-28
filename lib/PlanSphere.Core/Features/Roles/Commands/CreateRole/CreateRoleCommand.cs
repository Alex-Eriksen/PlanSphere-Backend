using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(SourceLevel SourceLevel, ulong SourceLevelId, ulong UserId, RoleRequest Request) : IRequest;