using MediatR;
using PlanSphere.Core.Features.Teams.DTOs;

namespace PlanSphere.Core.Features.Teams.Queries.GetTeam;

public record GetTeamQuery(ulong teamId) : IRequest<TeamDTO>
{
    
}