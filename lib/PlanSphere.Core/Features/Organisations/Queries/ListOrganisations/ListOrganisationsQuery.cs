using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Organisations.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;

public record ListOrganisationsQuery(string? Search, OrganisationSortBy OrganisationSortBy, bool SortDescending)
    : BasePaginatedQuery, IRequest<IPaginatedResponse<OrganisationDTO>>, ISearchableQuery,
        ISortableQuery<OrganisationSortBy>
{
    public OrganisationSortBy SortBy { get; init; }
}
