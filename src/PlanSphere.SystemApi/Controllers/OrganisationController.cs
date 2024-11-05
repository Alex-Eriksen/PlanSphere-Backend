using System.Security.Claims;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.PatchOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;
using PlanSphere.Core.Features.Organisations.Queries.GetOrganisation;
using PlanSphere.Core.Features.Organisations.Queries.GetOrganisationDetails;
using PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;
using PlanSphere.Core.Features.Organisations.Requests;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

public class OrganisationController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    [HttpGet("{organisationId?}", Name = nameof(GetOrganisationByIdAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View])]
    public async Task<IActionResult> GetOrganisationByIdAsync([FromRoute] ulong? organisationId)
    {
        if (organisationId == null)
        {
            return BadRequest("OrganisationId is null nd it cannot be");
        }
        var query = new GetOrganisationQuery(organisationId.Value);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet("{organisationId?}", Name = nameof(GetOrganisationDetailsByIdAsync))]
    //[TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View])]
    public async Task<IActionResult> GetOrganisationDetailsByIdAsync([FromRoute] ulong? organisationId)
    {
        if (organisationId == null)
        {
            return BadRequest("OrganisationId is null nd it cannot be");
        }
        var query = new GetOrganisationDetailsQuery(organisationId.Value);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet(Name = nameof(ListOrganisationsAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View])]
    public async Task<IActionResult> ListOrganisationsAsync([FromQuery] ListOrganisationsQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost(Name = nameof(CreateOrganisationAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator])]
    public async Task<IActionResult> CreateOrganisationAsync([FromBody] CreateOrganisationCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }

    [HttpPut(Name = nameof(UpdateOrganisationAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> UpdateOrganisationAsync([FromBody] UpdateOrganisationCommand command)
    {
        command.OrganisationId = _claims.GetOrganizationId();
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{organisationId?}", Name = nameof(DeleteOrganisationAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator])]
    public async Task<IActionResult> DeleteOrganisationAsync([FromRoute] ulong? organisationId)
    {
        if (organisationId == null)
        {
            return BadRequest("OrganisationId is null nd it cannot be");
        }
        var command = new DeleteOrganisationCommand(organisationId.Value);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{organisationId?}", Name = nameof(PatchOrganisationAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit])]
    public async Task<IActionResult> PatchOrganisationAsync([FromRoute] ulong? organisationId, [FromBody] JsonPatchDocument<OrganisationUpdateRequest> patchRequest)
    {
        if (organisationId == null)
        {
            return BadRequest("OrganisationId is null and it cannot be");
        }
        var command = new PatchOrganisationCommand(patchRequest);
        command.Id = organisationId.Value;
        await _mediator.Send(command);
        return Created();
    }
}