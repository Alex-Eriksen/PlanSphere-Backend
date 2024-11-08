using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Companies.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Companies.Queries.ListUserCompanies;

public record ListUserCompaniesQuery(string? Search, CompanySortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<CompanyDTO>>, ISearchableQuery, ISortableQuery<CompanySortBy>
{
    public ulong UserId { get; set; }
}