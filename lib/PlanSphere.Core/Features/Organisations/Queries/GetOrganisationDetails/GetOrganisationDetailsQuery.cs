using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.Organisations.DTOs;

namespace PlanSphere.Core.Features.Organisations.Queries.GetOrganisationDetails;

public record GetOrganisationDetailsQuery(ulong Id) : IRequest<OrganisationDetailDTO>
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
}
