using AutoMapper;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Teams.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Teams.Queries.LookUpTeams;

[HandlerType(HandlerType.SystemApi)]
public class LookUpTeamsQueryHandler(
    ILogger<LookUpTeamsQueryHandler> logger,
    IUserRepository userRepository,
    IMapper mapper,
    ITeamRepository teamRepository
) : IRequestHandler<LookUpTeamsQuery, List<TeamLookUpDTO>>
{
    private readonly ILogger<LookUpTeamsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));

    public async Task<List<TeamLookUpDTO>> Handle(LookUpTeamsQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Looking up teams.");
        _logger.LogInformation("Retrieving teams that the user with id: [{userId}] has access to.", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var userRoles = user.Roles.Select(x => x.Role);
        
        var enumerable = userRoles.ToList();
        var teamIds = enumerable
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Organisation)
            .SelectMany(x => x.Companies)
            .SelectMany(x => x.Departments)
            .SelectMany(x => x.Teams)
            .Select(x => x.Id)
            .ToList();
        
        teamIds.AddRange(enumerable
            .SelectMany(x => x.CompanyRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Company)
            .SelectMany(x => x.Departments)
            .SelectMany(x => x.Teams)
            .Select(x => x.Id)
            .ToList()
        );
        
        teamIds.AddRange(enumerable
            .SelectMany(x => x.DepartmentRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Department)
            .SelectMany(x => x.Teams)
            .Select(x => x.Id)
            .ToList()
        );
        
        teamIds.AddRange(enumerable
            .SelectMany(x => x.TeamRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.TeamId)
            .ToList());

        teamIds = teamIds.Distinct().ToList();
        var teams = await _teamRepository.GetQueryable().Where(x => teamIds.Contains(x.Id)).AsSplitQuery().ToListAsync(cancellationToken);
        _logger.LogInformation("Retrieved teams that the user with id: [{userId}] has access to.", request.UserId);

        var teamLookUpDtos = _mapper.Map<List<TeamLookUpDTO>>(teams);

        return teamLookUpDtos;
    }
}