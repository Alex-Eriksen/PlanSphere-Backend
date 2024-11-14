using System.Security.Claims;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Features.Rights.Queries.LookUp;
using PlanSphere.Core.Features.Roles.Commands.CreateRole;
using PlanSphere.Core.Features.Roles.Commands.DeleteRole;
using PlanSphere.Core.Features.Roles.Commands.ToggleInheritance;
using PlanSphere.Core.Features.Roles.Commands.UpdateRole;
using PlanSphere.Core.Features.Roles.Queries.GetRoleById;
using PlanSphere.Core.Features.Roles.Queries.ListRoles;
using PlanSphere.Core.Features.Roles.Queries.LookUpRoles;
using PlanSphere.Core.Features.Roles.Requests;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class RoleController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost("{sourceLevel}/{sourceLevelId}", Name = nameof(CreateRoleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator])]
    public async Task<IActionResult> CreateRoleAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromBody] RoleRequest request)
    {
        var command = new CreateRoleCommand(request, Request.HttpContext.User.GetUserId())
        {
            SourceLevel = sourceLevel,
            SourceLevelId = sourceLevelId,
        };
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
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator])]
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
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator])]
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

    [HttpGet("{sourceLevel}/{sourceLevelId}", Name = nameof(ListRolesAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View])]
    public async Task<IActionResult> ListRolesAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromQuery] ListRolesQuery query)
    {
        var organisationId = Request.HttpContext.User.GetOrganisationId();
        query.OrganisationId = organisationId;
        query.SourceLevelId = sourceLevelId;
        query.SourceLevel = sourceLevel;
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{sourceLevel}/{sourceLevelId}", Name = nameof(LookUpRolesAsync))]
    public async Task<IActionResult> LookUpRolesAsync([FromRoute] SourceLevel sourceLevel, ulong sourceLevelId, [FromQuery] LookUpRolesQuery query)
    {
        var organisationId = Request.HttpContext.User.GetOrganisationId();
        query.OrganisationId = organisationId;
        query.SourceLevel = sourceLevel;
        query.SourceLevelId = sourceLevelId;
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("{sourceLevel}/{sourceLevelId}/{roleId}", Name = nameof(ToggleRoleInheritanceAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator])]
    public async Task<IActionResult> ToggleRoleInheritanceAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromRoute] ulong roleId)
    {
        var command = new ToggleRoleInheritanceCommand(roleId) { SourceLevelId = sourceLevelId, SourceLevel = sourceLevel };
        await _mediator.Send(command);
        return NoContent();
    }
}