using MediatR;

namespace PlanSphere.Core.Features.Teams.Commands.DeleteTeam;

public record DeleteTeamCommand(ulong teamId) : IRequest;
    
