using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Teams.Commands.DeleteTeam;
using PlanSphere.Core.Features.Teams.Queries.GetTeam;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class TeamController (IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View])]
    [HttpGet("{teamId}/{sourceLevel}/{sourceLevelId}", Name = nameof(GetTeamById))]
    public async Task<IActionResult> GetTeamById([FromRoute] ulong teamId)
    {
        var query = new GetTeamQuery(teamId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Department])]
    [HttpDelete("{sourceLevelId}/{teamId}", Name = nameof(DeleteTeamAsync))]
    public async Task<IActionResult> DeleteTeamAsync([FromRoute] ulong sourceLevelId, [FromRoute] ulong teamId)
    {
        var command = new DeleteTeamCommand(teamId);
        await _mediator.Send(command);
        return NoContent();
    }

}