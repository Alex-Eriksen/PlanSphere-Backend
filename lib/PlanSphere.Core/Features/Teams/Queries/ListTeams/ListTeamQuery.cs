using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Teams.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Teams.Queries.ListTeams;

public record ListTeamQuery (string? Search, TeamSortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<TeamDTO>>, ISearchableQuery, ISortableQuery<TeamSortBy>
{
    [JsonIgnore]
    public ulong DepartmentId { get; set; }
}