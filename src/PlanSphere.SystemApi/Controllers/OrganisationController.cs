using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.PatchOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;
using PlanSphere.Core.Features.Organisations.Queries;
using PlanSphere.Core.Features.Organisations.Queries.GetOrganisation;
using PlanSphere.Core.Features.Organisations.Queries.GetOrganisationDetails;
using PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;
using PlanSphere.Core.Features.Organisations.Requests;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

public class OrganisationController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    [HttpGet("{organisationId}", Name = nameof(GetOrganisationByIdAsync))]
    public async Task<IActionResult> GetOrganisationByIdAsync([FromRoute] ulong organisationId)
    {
        var query = new GetOrganisationQuery(organisationId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet("{organisationId}", Name = nameof(GetOrganisationDetailsByIdAsync))]
    public async Task<IActionResult> GetOrganisationDetailsByIdAsync([FromRoute] ulong organisationId)
    {
        var query = new GetOrganisationDetailsQuery(organisationId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet(Name = nameof(ListOrganisationsAsync))]
    public async Task<IActionResult> ListOrganisationsAsync([FromQuery] ListOrganisationsQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost(Name = nameof(CreateOrganisationAsync))]
    public async Task<IActionResult> CreateOrganisationAsync([FromBody] CreateOrganisationCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }

    [HttpPut(Name = nameof(UpdateOrganisationAsync))]
    public async Task<IActionResult> UpdateOrganisationAsync([FromBody] UpdateOrganisationCommand command)
    {
        command.OrganisationId = _claims.GetOrganizationId();
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{organisationId}", Name = nameof(DeleteOrganisationAsync))]
    public async Task<IActionResult> DeleteOrganisationAsync([FromRoute] ulong organisationId)
    {
        var command = new DeleteOrganisationCommand(organisationId);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{organisationId}", Name = nameof(PatchOrganisationAsync))]
    public async Task<IActionResult> PatchOrganisationAsync([FromRoute] ulong organisationId, [FromBody] JsonPatchDocument<OrganisationUpdateRequest> patchRequest)
    {
        var command = new PatchOrganisationCommand(patchRequest);
        command.Id = organisationId;
        await _mediator.Send(command);
        return Created();
    }
}