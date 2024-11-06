using System.Text.Json.Serialization;
using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Departments.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Departments.Queries.ListDepartments;

public record ListDepartmentQuery(string? Search, DepartmentSortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<DepartmentDTO>>, ISearchableQuery, ISortableQuery<DepartmentSortBy>
{
    [JsonIgnore]
    public ulong CompanyId { get; set; }
}