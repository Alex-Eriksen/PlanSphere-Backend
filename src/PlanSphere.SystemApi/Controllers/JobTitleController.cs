using System.Security.Claims;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Features.JobTitles.Commands.AssignJobTitle;
using PlanSphere.Core.Features.JobTitles.Commands.CreateJobTitle;
using PlanSphere.Core.Features.JobTitles.Commands.DeleteJobTitle;
using PlanSphere.Core.Features.JobTitles.Commands.ToggleJobTitleInheritance;
using PlanSphere.Core.Features.JobTitles.Commands.UpdateJobTitle;
using PlanSphere.Core.Features.JobTitles.Queries.GetJobTitle;
using PlanSphere.Core.Features.JobTitles.Queries.ListJobTitles;
using PlanSphere.Core.Features.JobTitles.Queries.LookUpJobTitles;
using PlanSphere.Core.Features.JobTitles.Requests;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class JobTitleController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext?.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    
    [HttpGet("{jobTitleId}", Name = nameof(GetJobTitleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.PureView])]
    public async Task<IActionResult> GetJobTitleAsync([FromRoute] ulong jobTitleId)
    {
        var query = new GetJobTitleQuery(jobTitleId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet("{sourceLevel}/{sourceLevelId}", Name = nameof(ListJobTitleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View])]
    public async Task<IActionResult> ListJobTitleAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromQuery] ListJobTitlesQuery query)
    {
        query.SourceLevel = sourceLevel;
        query.SourceLevelId = sourceLevelId;
        query.OrganisationId = _claims.GetOrganisationId();
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost("{sourceLevel}/{sourceLevelId}", Name = nameof(CreateJobTitleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> CreateJobTitleAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromBody] JobTitleRequest request)
    {
        var command = new CreateJobTitleCommand(request)
        {
            SourceLevel = sourceLevel,
            SourceLevelId = sourceLevelId
        };
        await _mediator.Send(command);
        return Created();
    }
    
    [HttpPut("{sourceLevel}/{sourceLevelId}/{jobTitleId}", Name = nameof(UpdateJobTitleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> UpdateJobTitleAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromRoute] ulong jobTitleId, [FromBody] JobTitleRequest request)
    {
        var command = new UpdateJobTitleCommand(request)
        {
            SourceLevel = sourceLevel,
            SourceLevelId = sourceLevelId,
            Id = jobTitleId
        };
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("{jobTitleId}", Name = nameof(DeleteJobTitleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> DeleteJobTitleAsync([FromRoute] ulong jobTitleId, [FromBody] DeleteJobTitleCommand command)
    {
        command.Id = jobTitleId;
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPost("{sourceLevel}/{sourceLevelId}/{jobTitleId}", Name = nameof(ToggleInheritanceAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> ToggleInheritanceAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromRoute] ulong jobTitleId)
    {
        var command = new ToggleJobTitleInheritanceCommand(jobTitleId)
        {
            SourceLevel = sourceLevel,
            SourceLevelId = sourceLevelId
        };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("{sourceLevel}/{sourceLevelId}/{jobTitleId}/{userId}", Name = nameof(AssignJobTitleAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.ManageUsers, true])]
    public async Task<IActionResult> AssignJobTitleAsync(ulong jobTitleId, ulong userId)
    {
        var command = new AssignJobTitleCommand(jobTitleId, userId);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet(Name = nameof(LookUpJobTitlesAsync))]
    public async Task<IActionResult> LookUpJobTitlesAsync()
    {
        var query = new LookUpJobTitlesCommand(Request.HttpContext.User.GetUserId());
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}