using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Teams.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Teams.Queries.ListUserTeams;

public record ListUserTeamQuery(string? Search, TeamSortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<TeamDTO>>, ISearchableQuery, ISortableQuery<TeamSortBy>
{
    public ulong UserId { get; set; }
}