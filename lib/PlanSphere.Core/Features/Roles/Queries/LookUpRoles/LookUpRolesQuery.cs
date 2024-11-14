using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Roles.Queries.LookUpRoles;

public record LookUpRolesQuery(ulong UserId) : IRequest<List<RoleLookUpDTO>>;