using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Teams.Request;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Teams.Commands.PatchTeam;

[HandlerType(HandlerType.SystemApi)]
public class PatchTeamCommandHandler(
    ITeamRepository teamRepository,
    ILogger<PatchTeamCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<PatchTeamCommand>
{
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
    private readonly ILogger<PatchTeamCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(PatchTeamCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching Team");
        _logger.LogInformation("Fetching team with id: [{teamId}]", command.TeamId);
        var team = await _teamRepository.GetByIdAsync(command.TeamId, cancellationToken);
        _logger.LogInformation("Fetched team with id: [{teamId}]", command.TeamId);

        var teamPatchRequest = _mapper.Map<TeamPatchRequest>(team);
        
        command.PatchDocument.ApplyTo(teamPatchRequest);

        team = _mapper.Map(teamPatchRequest, team);

        if (team.InheritAddress)
        {
            team.Address.ParentId = team.Department.AddressId;
        }
        else
        {
            team.Address.Parent = null;
            team.Address.ParentId = null;
        }

        if (team.Settings.InheritDefaultWorkSchedule)
        {
            team.Settings.DefaultWorkSchedule.ParentId = team.Department.Settings.DefaultWorkScheduleId;
        }
        else
        {
            team.Settings.DefaultWorkSchedule.Parent = null;
            team.Settings.DefaultWorkSchedule.ParentId = null;
        }

        _logger.LogInformation("Patching team with id: [{teamId}]", command.TeamId);
        await _teamRepository.UpdateAsync(command.TeamId, team, cancellationToken);
        _logger.LogInformation("Patched team with id: [{teamId}]", command.TeamId);
    }
}