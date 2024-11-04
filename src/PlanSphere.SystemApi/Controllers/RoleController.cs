using System.Security.Claims;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Rights.Queries.LookUp;
using PlanSphere.Core.Features.Roles.Commands.CreateRole;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class RoleController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext?.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    [HttpPost("{sourceLevel}/{sourceLevelId}", Name = nameof(CreateRoleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> CreateRoleAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromBody] CreateRoleCommand command)
    {
        command.SourceLevel = sourceLevel;
        command.SourceLevelId = sourceLevelId;
        command.UserId = _claims.GetUserId();
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
}