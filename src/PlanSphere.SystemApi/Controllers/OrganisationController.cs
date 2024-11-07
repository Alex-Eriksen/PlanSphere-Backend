using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Organisations.Queries.LookUp;
using PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.PatchOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;
using PlanSphere.Core.Features.Organisations.Queries.GetOrganisation;
using PlanSphere.Core.Features.Organisations.Queries.ListOrganisations;
using PlanSphere.Core.Features.Organisations.Requests;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class OrganisationController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [HttpGet(Name = nameof(LookUpOrganisationsAsync))]
    public async Task<IActionResult> LookUpOrganisationsAsync()
    {
        var userId = Request.HttpContext.User.GetUserId();
        var query = new LookUpOrganisationsQuery(userId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet("{sourceLevelId?}", Name = nameof(GetOrganisationByIdAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Organisation])]
    public async Task<IActionResult> GetOrganisationByIdAsync([FromRoute] ulong? sourceLevelId)
    {
        var selectedId = sourceLevelId ?? Request.HttpContext.User.GetOrganizationId();
        
        var query = new GetOrganisationQuery(selectedId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet(Name = nameof(ListOrganisationsAsync))]
    [TypeFilter(typeof(SystemAdministratorFilter))]
    public async Task<IActionResult> ListOrganisationsAsync([FromQuery] ListOrganisationsQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost(Name = nameof(CreateOrganisationAsync))]
    [TypeFilter(typeof(SystemAdministratorFilter))]
    public async Task<IActionResult> CreateOrganisationAsync([FromBody] OrganisationRequest request)
    {
        var command = new CreateOrganisationCommand(request);
        await _mediator.Send(command);
        return Created();
    }

    [HttpPut("{sourceLevelId?}",Name = nameof(UpdateOrganisationAsync))]
    //[TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Organisation])]
    public async Task<IActionResult> UpdateOrganisationAsync([FromRoute] ulong? sourceLevelId, [FromBody] OrganisationRequest request)
    {
        var command = new UpdateOrganisationCommand(request, sourceLevelId ?? Request.HttpContext.User.GetOrganizationId());
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{sourceLevelId}", Name = nameof(DeleteOrganisationAsync))]
    //[TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator, SourceLevel.Organisation])]
    public async Task<IActionResult> DeleteOrganisationAsync([FromRoute] ulong sourceLevelId)
    {
        var command = new DeleteOrganisationCommand(sourceLevelId);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{sourceLevelId}", Name = nameof(PatchOrganisationAsync))]
    //[TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Organisation])]
    public async Task<IActionResult> PatchOrganisationAsync([FromRoute] ulong? sourceLevelId, [FromBody] JsonPatchDocument<OrganisationRequest> patchRequest)
    {
        var selectedId = sourceLevelId ?? Request.HttpContext.User.GetOrganizationId();
        var command = new PatchOrganisationCommand(patchRequest, selectedId);
        await _mediator.Send(command);
        return NoContent();
    }
}