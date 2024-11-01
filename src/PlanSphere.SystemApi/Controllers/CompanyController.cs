using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Companies.Commands.CreateCompany;
using PlanSphere.Core.Features.Companies.Commands.DeleteCompany;
using PlanSphere.Core.Features.Companies.DTOs;
using PlanSphere.Core.Features.Companies.Qurries.GetCompany;
using PlanSphere.Core.Features.Companies.Qurries.ListCompanies;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

    
    public class CompanyController(IMediator mediator) : ApiControllerBase(mediator)
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        
        [HttpGet("{companyId}", Name = nameof(GetCompanyById))]
        public async Task<IActionResult> GetCompanyById([FromRoute] ulong companyId)
        {
            var query = new GetCompanyQuery(companyId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        
        [HttpGet(Name = nameof(ListCompaniesAsync))]
        public async Task<IActionResult> ListCompaniesAsync([FromQuery] ListCompaniesQuery query)
        {
            query.OrganisationId = Request.HttpContext.User.GetOrganizationId();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpPost(Name = nameof(CreateCompanyAsync))]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CreateCompanyCommand command)
        {
            command.OrganisationId = Request.HttpContext.User.GetOrganizationId();
            await _mediator.Send(command);
            return Created();
        }
        [HttpPatch("{companyId}", Name = nameof(PatchCompanyAsync))] 
        public async Task<IActionResult> PatchCompanyAsync([FromRoute] ulong organisationId, [FromBody] CreateCompanyCommand command)
        {
            command.OrganisationId = organisationId;
            await _mediator.Send(command);
            return Created();
        }
        [HttpDelete("{companyId}", Name = nameof(DeleteCompanyAsync))] 
        public async Task<IActionResult> DeleteCompanyAsync([FromRoute] ulong companyId)
        {
            var command = new DeleteCompanyCommand(companyId);
            await _mediator.Send(command);
            return NoContent();
        }
        
        

    }
    
    
    

