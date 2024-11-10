using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Teams.Commands.DeleteTeam;

[HandlerType(HandlerType.SystemApi)]
public class DeleteTeamCommandHandler(
    ITeamRepository teamRepository,
    ILogger<DeleteTeamCommandHandler> logger
) : IRequestHandler<DeleteTeamCommand>
{
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
    private readonly ILogger<DeleteTeamCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Deleting Team");
        _logger.LogInformation("Deleting Team with [{id}]", request.teamId);
        await _teamRepository.DeleteAsync(request.teamId, cancellationToken);
        _logger.LogInformation("Deleted Team with id [{id}]", request.teamId);
    }
}