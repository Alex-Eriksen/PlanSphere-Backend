using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.JobTitles.Commands.CreateJobTitle;
using PlanSphere.Core.Features.JobTitles.Commands.DeleteJobTitle;
using PlanSphere.Core.Features.JobTitles.Commands.UpdateJobTitle;
using PlanSphere.Core.Features.JobTitles.Organisation.Queries.ListOrganisationJobTitles;
using PlanSphere.Core.Features.JobTitles.Queries.GetJobTitle;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

public class JobTitleController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("{jobTitleId}", Name = nameof(GetJobTitleAsync))]
    public async Task<IActionResult> GetJobTitleAsync([FromRoute] ulong jobTitleId)
    {
        var query = new GetJobTitleQuery(jobTitleId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet(Name = nameof(ListJobTitleAsync))]
    public async Task<IActionResult> ListJobTitleAsync()
    {
        return Ok();
    }
    
    [HttpPost(Name = nameof(CreateJobTitleAsync))]
    public async Task<IActionResult> CreateJobTitleAsync([FromBody] CreateJobTitleCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }
    
    [HttpPut("{jobTitleId}", Name = nameof(UpdateJobTitleAsync))]
    public async Task<IActionResult> UpdateJobTitleAsync([FromRoute] ulong jobTitleId, [FromBody] UpdateJobTitleCommand command)
    {
        command.Id = jobTitleId;
        await _mediator.Send(command);
        return Created();
    }
    
    [HttpDelete("{jobTitleId}", Name = nameof(DeleteJobTitleAsync))]
    public async Task<IActionResult> DeleteJobTitleAsync([FromRoute] ulong jobTitleId, [FromBody] DeleteJobTitleCommand command)
    {
        command.Id = jobTitleId;
        await _mediator.Send(command);
        return Created();
    }
}