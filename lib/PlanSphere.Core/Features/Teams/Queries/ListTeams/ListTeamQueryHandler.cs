using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Teams.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;

namespace PlanSphere.Core.Features.Teams.Queries.ListTeams;

[HandlerType(HandlerType.SystemApi)]
public class ListTeamQueryHandler(
    ITeamRepository teamRepository,
    ILogger<ListTeamQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListTeamQuery, IPaginatedResponse<TeamDTO>>
{
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
    private readonly ILogger<ListTeamQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));

    public async Task<IPaginatedResponse<TeamDTO>> Handle(ListTeamQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching Teams");
        _logger.LogInformation("Fetching Teams from department with id: [{departmentId}]", request.DepartmentId);
        var query = _teamRepository.GetQueryable().Where(x => x.DepartmentId == request.DepartmentId);
        _logger.LogInformation("Fetched teams from department with id: [{departmentId}]", request.DepartmentId);
        
        query = SearchQuery(request, query);
        query = SortQuery(request, query);

        var paginatedRespone = await _paginationService.PaginateAsync<Team, TeamDTO>(query, request);

        return paginatedRespone;
    }
    
    private IQueryable<Team> SearchQuery(ListTeamQuery request, IQueryable<Team> query)
    {
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower().Trim();
            query = query.Where(d => d.Name.ToLower().Contains(search) ||
                                     (d.Address.StreetName.ToLower() + " " + d.Address.HouseNumber.ToLower()).Contains(search));

        }

        return query;
    }
    
    private IQueryable<Team> SortQuery(ListTeamQuery request, IQueryable<Team> query)
    {
        return request.SortBy switch
        {
            TeamSortBy.Name => query.OrderByExpression(x => x.Name, request.SortDescending),
            TeamSortBy.Address => query.OrderByExpression(x => x.Address.StreetName, request.SortDescending)
                .ThenByExpression(x => x.Address.HouseNumber, request.SortDescending)
        };
    }
}