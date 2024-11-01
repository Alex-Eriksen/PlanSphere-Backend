using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;
using PlanSphere.Core.Features.Organisations.Queries;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers.Organisation;

public class OrganisationController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("{organisationId}", Name = nameof(GetOrganisationAsync))]
    public async Task<IActionResult> GetOrganisationAsync([FromRoute] ulong organisationId)
    {
        var query = new GetOrganisationQuery(organisationId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet(Name = nameof(ListOrganisationAsync))]
    public async Task<IActionResult> ListOrganisationAsync()
    {
        return Ok();
    }
    
    [HttpPost(Name = nameof(CreateOrganisationAsync))]
    public async Task<IActionResult> CreateOrganisationAsync([FromBody] CreateOrganisationCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }

    [HttpPut("{organisationId}", Name = nameof(UpdateOrganisationAsync))]
    public async Task<IActionResult> UpdateOrganisationAsync([FromRoute] ulong organisationId,
        [FromBody] UpdateOrganisationCommand command)
    {
        command.Id = organisationId;
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{organisationId}", Name = nameof(DeleteOrganisationAsync))]
    public async Task<IActionResult> DeleteOrganisationAsync([FromRoute] ulong organisationId, [FromBody] DeleteOrganisationCommand command)
    {
        command.Id = organisationId;
        await _mediator.Send(command);
        return Ok();
    }
}