using System.Text.Json.Serialization;
using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Companies.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Companies.Queries.ListCompanies;

public record ListCompaniesQuery(string? Search, CompanySortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<CompanyDTO>>, ISearchableQuery, ISortableQuery<CompanySortBy>
{
    [JsonIgnore]
    public ulong OrganisationId {  get; set; }
}