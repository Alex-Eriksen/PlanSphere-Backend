using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Organisations.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;

public record ListOrganisationsQuery(string? Search, bool SortDescending): BasePaginatedQuery, IRequest<IPaginatedResponse<OrganisationDTO>>, ISearchableQuery, ISortableQuery<OrganisationSortBy>
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
    public OrganisationSortBy SortBy { get; init; }
}
