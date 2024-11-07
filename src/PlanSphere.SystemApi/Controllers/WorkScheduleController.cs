using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.WorkSchedules.Commands;
using PlanSphere.Core.Features.WorkSchedules.Queries.GetWorkScheduleById;
using PlanSphere.Core.Features.WorkSchedules.Queries.LookUpWorkSchedules;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class WorkScheduleController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    

    [HttpPost("{organisationId}", Name = nameof(CreateWorkScheduleAsync))]
    public async Task<IActionResult> CreateWorkScheduleAsync([FromRoute] ulong organisationId, SourceLevel sourceLevel, ulong sourceLevelId, [FromBody] CreateWorkScheduleCommand command)
    {
        command.OrganisationId = organisationId;
        command.SourceLevel = sourceLevel;
        command.SourceLevelId = sourceLevelId;
        await _mediator.Send(command);
        return NoContent();
    }

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
}
    
    
