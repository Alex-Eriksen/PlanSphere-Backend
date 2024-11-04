using System.Security.Claims;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Rights.Queries.LookUp;
using PlanSphere.Core.Features.Roles.Commands.CreateRole;
using PlanSphere.Core.Features.Roles.Commands.DeleteRole;
using PlanSphere.Core.Features.Roles.Commands.UpdateRole;
using PlanSphere.Core.Features.Roles.Queries.GetRoleById;
using PlanSphere.Core.Features.Roles.Requests;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class RoleController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("{sourceLevel}/{sourceLevelId}", Name = nameof(CreateRoleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> CreateRoleAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromBody] CreateRoleCommand command)
    {
        command.SourceLevel = sourceLevel;
        command.SourceLevelId = sourceLevelId;
        command.UserId = Request.HttpContext.User.GetUserId();
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet(Name = nameof(LookUpRightsAsync))]
    public async Task<IActionResult> LookUpRightsAsync()
    {
        var query = new LookUpRightsQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpPut("{sourceLevel}/{sourceLevelId}/{roleId}", Name = nameof(UpdateRoleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> UpdateRoleAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromRoute] ulong roleId, [FromBody] RoleRequest request)
    {
        var userId = Request.HttpContext.User.GetUserId();
        var command = new UpdateRoleCommand(roleId, userId, request);
        command.SourceLevel = sourceLevel;
        command.SourceLevelId = sourceLevelId;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{sourceLevel}/{sourceLevelId}/{roleId}", Name = nameof(DeleteRoleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> DeleteRoleAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromRoute] ulong roleId)
    {
        var userId = Request.HttpContext.User.GetUserId();
        var command = new DeleteRoleCommand(userId, roleId)
        {
            SourceLevel = sourceLevel,
            SourceLevelId = sourceLevelId
        };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{sourceLevel}/{sourceLevelId}/{roleId}", Name = nameof(GetRoleByIdAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View])]
    public async Task<IActionResult> GetRoleByIdAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromRoute] ulong roleId)
    {
        var query = new GetRoleByIdQuery(sourceLevel, sourceLevelId, roleId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}