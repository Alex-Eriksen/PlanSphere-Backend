using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Teams.DTOs;
using PlanSphere.Core.Features.Teams.Queries.ListTeams;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Teams.Queries.ListUserTeams;

[HandlerType(HandlerType.SystemApi)]
public class ListUserTeamQueryHandler(
    ITeamRepository teamRepository,
    IUserRepository userRepository,
    ILogger<ListUserTeamQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListUserTeamQuery, IPaginatedResponse<TeamDTO>>
{
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<ListUserTeamQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));

    public async Task<IPaginatedResponse<TeamDTO>> Handle(ListUserTeamQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching Teams");
        _logger.LogInformation("Fetching teams from user with id: [{userId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var userRoles = user.Roles.Select(x => x.Role);

        var teamIds = userRoles
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Organisation)
            .SelectMany(x => x.Companies)
            .SelectMany(x => x.Departments)
            .SelectMany(x => x.Teams)
            .Select(x => x.Id)
            .ToList();

        teamIds.AddRange(userRoles
            .SelectMany(x => x.CompanyRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Company)
            .SelectMany(x => x.Departments)
            .SelectMany(x => x.Teams)
            .Select(x => x.Id)
            .ToList()
        );
        
        teamIds.AddRange(userRoles
            .SelectMany(x => x.DepartmentRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Department)
            .SelectMany(x => x.Teams)
            .Select(x => x.Id)
            .ToList()
        );
        
        teamIds.AddRange(userRoles
            .SelectMany(x => x.TeamRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Team.Id)
            .ToList()
        );

        teamIds = teamIds.Distinct().ToList();

        var query = _teamRepository.GetQueryable().Where(x => teamIds.Contains(x.Id));
        _logger.LogInformation("Fetched teams from user with id: [{userId}]", request.UserId);
        
        query = SearchQuery(request, query);
        query = SortQuery(request, query);

        var paginatedRespone = await _paginationService.PaginateAsync<Team, TeamDTO>(query, request,opt => opt.Items[MappingKeys.InheritAddress] = true);

        return paginatedRespone;
    }
    
    private IQueryable<Team> SearchQuery(ListUserTeamQuery request, IQueryable<Team> query)
    {
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower().Trim();
            query = query.Where(d => d.Name.ToLower().Contains(search) ||
                                     (d.Address.StreetName.ToLower() + " " + d.Address.HouseNumber.ToLower()).Contains(search));

        }

        return query;
    }
    
    private IQueryable<Team> SortQuery(ListUserTeamQuery request, IQueryable<Team> query)
    {
        return request.SortBy switch
        {
            TeamSortBy.Name => query.OrderByExpression(x => x.Name, request.SortDescending),
            TeamSortBy.StreetName => query.OrderByExpression(x => x.Address.StreetName, request.SortDescending),
            TeamSortBy.HouseNumber => query.OrderByExpression(x => x.Address.HouseNumber, request.SortDescending)
        };
    }
}