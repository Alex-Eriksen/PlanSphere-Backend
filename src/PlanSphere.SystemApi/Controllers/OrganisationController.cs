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
using PlanSphere.Core.Features.Organisations.Queries.GetOrganisationDetails;
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
    
    [HttpGet("{sourceLevel}/{sourceLevelId?}", Name = nameof(GetOrganisationByIdAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Organisation])]
    public async Task<IActionResult> GetOrganisationByIdAsync([FromRoute] SourceLevel sourceLevel, ulong? sourceLevelId)
    {
        if (sourceLevelId == null)
        {
            sourceLevelId = Request.HttpContext.User.GetOrganizationId();
        }
        
        var query = new GetOrganisationQuery(sourceLevelId.Value);
        query.SourceLevelId = sourceLevelId.Value;
        query.SourceLevel = sourceLevel;
        
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet("{sourceLevel}/{sourceLevelId?}", Name = nameof(GetOrganisationDetailsByIdAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Organisation])]
    public async Task<IActionResult> GetOrganisationDetailsByIdAsync([FromRoute] SourceLevel sourceLevel, ulong? sourceLevelId)
    {
        if (sourceLevelId == null)
        {
            sourceLevelId = Request.HttpContext.User.GetOrganizationId();
        }
        var query = new GetOrganisationDetailsQuery(sourceLevelId.Value);
        query.SourceLevelId = sourceLevelId.Value;
        query.SourceLevel = sourceLevel;
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{sourceLevel}/{sourceLevelId}", Name = nameof(ListOrganisationsAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Organisation])]
    public async Task<IActionResult> ListOrganisationsAsync([FromRoute] SourceLevel sourceLevel, ulong sourceLevelId, [FromQuery] ListOrganisationsQuery query)
    {
        query.SourceLevel = sourceLevel;
        query.SourceLevelId = sourceLevelId;
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

    [HttpPut("{sourceLevel}/{sourceLevelId}/{organisationId}",Name = nameof(UpdateOrganisationAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Organisation])]
    public async Task<IActionResult> UpdateOrganisationAsync([FromRoute] SourceLevel sourceLevel, ulong sourceLevelId, ulong? organisationId, [FromBody] OrganisationRequest request)
    {
        var command = new UpdateOrganisationCommand(request)
        {
            SourceLevel = sourceLevel,
            SourceLevelId = sourceLevelId,
            OrganisationId = organisationId.Value
        };

        if (command.OrganisationId == null)
        {
            command.OrganisationId = Request.HttpContext.User.GetOrganizationId();
        }
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{organisationId?}", Name = nameof(DeleteOrganisationAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator, SourceLevel.Organisation])]
    public async Task<IActionResult> DeleteOrganisationAsync([FromRoute] ulong? organisationId)
    {
        var selectedId = organisationId ?? Request.HttpContext.User.GetOrganizationId();
        
        var command = new DeleteOrganisationCommand(selectedId);
        
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{sourceLevelId}/{organisationId?}", Name = nameof(PatchOrganisationAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Organisation])]
    public async Task<IActionResult> PatchOrganisationAsync([FromRoute] ulong sourceLevelId, ulong? organisationId, [FromBody] JsonPatchDocument<OrganisationRequest> patchRequest)
    {
        var selectedId = organisationId ?? Request.HttpContext.User.GetOrganizationId();

        var command = new PatchOrganisationCommand(patchRequest, selectedId);
        await _mediator.Send(command);
        return NoContent();
    }
}