using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Metrics;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Features.WorkSchedules.Commands;
using PlanSphere.Core.Features.WorkSchedules.Commands.UpdateWorkSchedule;
using PlanSphere.Core.Features.WorkSchedules.Queries.GetWorkScheduleById;
using PlanSphere.Core.Features.WorkSchedules.Queries.LookUpWorkSchedules;
using PlanSphere.Core.Features.WorkSchedules.Request;
using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class WorkScheduleController(IMediator mediator, IWorkScheduleFilter workScheduleFilter, IRoleFilter roleFilter) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IWorkScheduleFilter _workScheduleFilter = workScheduleFilter ?? throw new ArgumentNullException(nameof(workScheduleFilter));
    private readonly IRoleFilter _roleFilter = roleFilter ?? throw new ArgumentNullException(nameof(roleFilter));

    /// <summary>
    /// Used for getting the available work schedules for a user.
    /// <br/>
    /// Doesn't provide a true list of work schedules in the system.
    /// Simply what user can choose to inherit from.
    /// </summary>
    /// <returns></returns>
    [HttpGet(Name = nameof(LookUpWorkSchedulesAsync))]
    public async Task<IActionResult> LookUpWorkSchedulesAsync()
    {
        var query = new LookUpWorkSchedulesQuery(Request.HttpContext.User.GetUserId());
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{workScheduleId}", Name = nameof(GetWorkScheduleByIdAsync))]
    public async Task<IActionResult> GetWorkScheduleByIdAsync([FromRoute] ulong workScheduleId)
    {
        var query = new GetWorkScheduleByIdQuery(workScheduleId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPut("{sourceLevel}/{sourceLevelId}/{workScheduleId?}", Name = nameof(UpdateWorkScheduleAsync))]
    public async Task<IActionResult> UpdateWorkScheduleAsync(SourceLevel sourceLevel, ulong sourceLevelId, ulong? workScheduleId, WorkScheduleRequest request)
    {
        if (workScheduleId == null && !await _workScheduleFilter.IsAllowedToChangeOwnWorkScheduleAsync(Request.HttpContext.User.GetUserId()))
        {
            throw new UnauthorizedAccessException(ErrorMessageConstants.UnauthorizedActionMessage);
        }
        else if(workScheduleId != null)
        {
            await _roleFilter.CheckIsAuthorizedSourceLevelAsync(Request.HttpContext, Right.Edit);
        }

        var command = new UpdateWorkScheduleCommand(workScheduleId, Request.HttpContext.User.GetUserId(), request);
        await _mediator.Send(command);
        return NoContent();
    }
}
    
    
