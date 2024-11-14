using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Features.WorkTimes.Commands.CheckInWorkTime;
using PlanSphere.Core.Features.WorkTimes.Commands.CheckOutWorkTime;
using PlanSphere.Core.Features.WorkTimes.Commands.CreateWorkTime;
using PlanSphere.Core.Features.WorkTimes.Commands.DeleteWorkTime;
using PlanSphere.Core.Features.WorkTimes.Commands.UpdateWorkTime;
using PlanSphere.Core.Features.WorkTimes.Queries.GetWorkTimes;
using PlanSphere.Core.Features.WorkTimes.Requests;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class WorkTimeController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet(Name = nameof(GetWorkTimesAsync))]
    public async Task<IActionResult> GetWorkTimesAsync([FromQuery] GetWorkTimesQuery query)
    {
        query.UserId = Request.HttpContext.User.GetUserId();
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost(Name = nameof(CreateWorkTimeAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.ManuallySetOwnWorkTime, true])]
    public async Task<IActionResult> CreateWorkTimeAsync([FromBody] WorkTimeRequest request)
    {
        var userId = Request.HttpContext.User.GetUserId();
        var command = new CreateWorkTimeCommand(request, userId);
        await _mediator.Send(command);
        return Created();
    }
    
    [HttpPut("{workTimeId}", Name = nameof(UpdateWorkTimeAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.ManuallySetOwnWorkTime, true])]
    public async Task<IActionResult> UpdateWorkTimeAsync([FromRoute] ulong workTimeId, [FromBody] WorkTimeRequest request)
    {
        var userId = Request.HttpContext.User.GetUserId();
        var command = new UpdateWorkTimeCommand(workTimeId, userId, request);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPost(Name = nameof(CheckInAsync))]
    public async Task<IActionResult> CheckInAsync()
    {
        var userId = Request.HttpContext.User.GetUserId();
        var command = new CheckInWorkTimeCommand(userId);
        await _mediator.Send(command);
        return Created();
    }
    
    [HttpPost(Name = nameof(CheckOutAsync))]
    public async Task<IActionResult> CheckOutAsync()
    {
        var userId = Request.HttpContext.User.GetUserId();
        var command = new CheckOutWorkTimeCommand(userId);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{workTimeId}", Name = nameof(DeleteWorkTimeAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.ManuallySetOwnWorkTime, true])]
    public async Task<IActionResult> DeleteWorkTimeAsync([FromRoute] ulong workTimeId)
    {
        var userId = Request.HttpContext.User.GetUserId();
        var command = new DeleteWorkTimeCommand(workTimeId, userId);
        await _mediator.Send(command);
        return NoContent();
    }
}