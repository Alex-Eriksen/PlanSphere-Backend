using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;
using PlanSphere.Core.Features.Organisations.Queries;
using PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

[Route("api/[controller]")]
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

    [HttpDelete(Name = nameof(DeleteOrganisationAsync))]
    public async Task<IActionResult> DeleteOrganisationAsync([FromBody] DeleteOrganisationCommand command)
    {
        command = command with { OrganisationId = _claims.GetOrganizationId() };
        await _mediator.Send(command);
        return NoContent();
    }
}