using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Companies.Commands.CreateCompany;
using PlanSphere.Core.Features.Companies.Commands.DeleteCompany;
using PlanSphere.Core.Features.Companies.Commands.PatchCompany;
using PlanSphere.Core.Features.Companies.Commands.UploadCompanyLogo;
using PlanSphere.Core.Features.Companies.Queries.GetCompany;
using PlanSphere.Core.Features.Companies.Queries.ListCompanies;
using PlanSphere.Core.Features.Companies.Queries.LookUp;
using PlanSphere.Core.Features.Companies.Request;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;
    
[Authorize]
public class CompanyController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Company])]
    [HttpGet("{sourceLevelId}/{companyId}", Name = nameof(GetCompanyById))]
    public async Task<IActionResult> GetCompanyById([FromRoute] ulong sourceLevelId,[FromRoute] ulong companyId)
    {
        var query = new GetCompanyQuery(companyId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpGet("{sourceLevelId}",Name = nameof(ListCompaniesAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Company])]
    public async Task<IActionResult> ListCompaniesAsync([FromRoute] ulong sourceLevelId,[FromQuery] ListCompaniesQuery query)
    {
        query.OrganisationId = Request.HttpContext.User.GetOrganizationId();
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost("{sourceLevelId}", Name = nameof(CreateCompanyAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Organisation])]
    public async Task<IActionResult> CreateCompanyAsync([FromRoute] ulong sourceLevelId,[FromBody] CompanyRequest request)
    {
        var command = new CreateCompanyCommand(request);
        command.OrganisationId = Request.HttpContext.User.GetOrganizationId();
        await _mediator.Send(command);
        return Created();
    }
    
    [HttpPatch("{sourceLevelId}", Name = nameof(PatchCompanyAsync))] 
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Company])]
    public async Task<IActionResult> PatchCompanyAsync([FromRoute] ulong sourceLevelId,[FromBody] JsonPatchDocument<CompanyUpdateRequest> patchRequest)
    {
        var command = new PatchCompanyCommand(patchRequest);
        command.Id = sourceLevelId;
        await _mediator.Send(command);
        return Created();
    }
    
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Edit, SourceLevel.Organisation])]
    [HttpDelete("{sourceLevelId}/{companyId}", Name = nameof(DeleteCompanyAsync))] 
    public async Task<IActionResult> DeleteCompanyAsync([FromRoute] ulong sourceLevelId,[FromRoute] ulong companyId)
    {
        var command = new DeleteCompanyCommand(companyId);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPost("{sourceLevelId}", Name = nameof(UploadCompanyLogoAsync))] 
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.Administrator, SourceLevel.Company])]
    public async Task<IActionResult> UploadCompanyLogoAsync([FromRoute] ulong sourceLevelId,[FromRoute] ulong companyId, [FromForm] UploadCompanyLogoCommand command)
    {
        command.OrganisationId = Request.HttpContext.User.GetOrganizationId();
        command.CompanyId = companyId;
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    [HttpGet("{sourceLevelId}",Name = nameof(LookUpCompaniesAsync))]
    [TypeFilter(typeof(RoleActionFilter), Arguments = [Right.View, SourceLevel.Organisation])]
    public async Task<IActionResult> LookUpCompaniesAsync()
    {
        var organisationId = Request.HttpContext.User.GetOrganizationId();
        var userId = Request.HttpContext.User.GetUserId();
        var query = new LookUpCompaniesQuery(organisationId, userId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}