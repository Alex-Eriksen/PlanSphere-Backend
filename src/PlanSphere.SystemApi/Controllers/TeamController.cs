using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Features.Teams.Commands.CreateTeam;
using PlanSphere.Core.Features.Teams.Commands.DeleteTeam;
using PlanSphere.Core.Features.Teams.Commands.PatchTeam;
using PlanSphere.Core.Features.Teams.Queries.GetTeam;
using PlanSphere.Core.Features.Teams.Queries.ListTeams;
using PlanSphere.Core.Features.Teams.Queries.ListUserTeams;
using PlanSphere.Core.Features.Teams.Queries.LookUpTeams;
using PlanSphere.Core.Features.Teams.Request;
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

    [HttpPost("{sourceLevelId}", Name = nameof(CreateTeamAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Department])]
    public async Task<IActionResult> CreateTeamAsync([FromRoute] ulong sourceLevelId, [FromBody] TeamRequest request)
    {
        var command = new CreateTeamCommand(request);
        command.DepartmentId = sourceLevelId;
        await _mediator.Send(command);
        return Created();
    }

    [HttpPatch("{sourceLevelId}", Name = nameof(PatchTeamAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Team])]
    public async Task<IActionResult> PatchTeamAsync([FromRoute] ulong sourceLevelId, [FromBody] JsonPatchDocument<TeamPatchRequest> patchRequest)
    {
        var command = new PatchTeamCommand(patchRequest);
        command.TeamId = sourceLevelId;
        await _mediator.Send(command);
        return NoContent();
    }

    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Department])]
    [HttpDelete("{sourceLevelId}/{teamId}", Name = nameof(DeleteTeamAsync))]
    public async Task<IActionResult> DeleteTeamAsync([FromRoute] ulong sourceLevelId, [FromRoute] ulong teamId)
    {
        var command = new DeleteTeamCommand(teamId);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{sourceLevelId}", Name = nameof(ListTeamAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Department])]
    public async Task<IActionResult> ListTeamAsync([FromRoute] ulong sourceLevelId, [FromQuery] ListTeamQuery query)
    {
        query.DepartmentId = sourceLevelId;
        var respone = await _mediator.Send(query);
        return Ok(respone);
    }

    [HttpGet(Name = nameof(ListUserTeamsAsync))]
    public async Task<IActionResult> ListUserTeamsAsync([FromQuery] ListUserTeamQuery query)
    {
        query.UserId = Request.HttpContext.User.GetUserId();
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet(Name = nameof(LookUpTeamsAsync))]
    public async Task<IActionResult> LookUpTeamsAsync()
    {
        var query = new LookUpTeamsQuery(Request.HttpContext.User.GetUserId());
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}