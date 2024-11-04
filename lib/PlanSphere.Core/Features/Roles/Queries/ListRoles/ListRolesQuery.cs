using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Roles.Queries.ListRoles;

public record ListRolesQuery(string? Search, RoleSortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<RoleListItemDTO>>, ISearchableQuery, ISortableQuery<RoleSortBy>
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
    public ulong OrganisationId { get; set; }
}
