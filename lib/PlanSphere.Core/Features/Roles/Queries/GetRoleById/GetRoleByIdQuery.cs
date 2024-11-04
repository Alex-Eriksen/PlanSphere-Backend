using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Roles.Queries.GetRoleById;

public record GetRoleByIdQuery(SourceLevel SourceLevel, ulong SourceLevelId, ulong RoleId) : IRequest<RoleDTO>;
