using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Departments.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Departments.Queries.ListUserDepartments;

public record ListUserDepartmentQuery(string? Search, DepartmentSortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<DepartmentDTO>>, ISearchableQuery, ISortableQuery<DepartmentSortBy>
{
    public ulong UserId { get; set; }
}