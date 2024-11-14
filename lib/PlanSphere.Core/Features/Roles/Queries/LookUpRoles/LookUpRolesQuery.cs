using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Roles.Queries.LookUpRoles;

public record LookUpRolesQuery() : IRequest<List<RoleLookUpDTO>>
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
    public ulong OrganisationId { get; set; }
}