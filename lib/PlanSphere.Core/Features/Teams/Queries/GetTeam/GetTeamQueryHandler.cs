using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Teams.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Teams.Queries.GetTeam;

[HandlerType(HandlerType.SystemApi)]
public class GetTeamQueryHandler(
    ITeamRepository teamRepository,
    ILogger<GetTeamQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<GetTeamQuery, TeamDTO>
{
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
    private readonly ILogger<GetTeamQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<TeamDTO> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        logger.BeginScope("Fetching Team");
        logger.LogInformation("Fetching Team with id: [{Id}]", request.TeamId);
        var team = await _teamRepository.GetByIdAsync(request.TeamId, cancellationToken);

        var mappedTeam = _mapper.Map<TeamDTO>(team);
        return mappedTeam;
    }
}