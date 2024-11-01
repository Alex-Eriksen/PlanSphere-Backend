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

namespace PlanSphere.SystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ApiControllerBase(mediator)
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext?.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        
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
            query.OrganisationId = _claims.GetOrganizationId();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpPost(Name = nameof(CreateCompanyAsync))]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CreateCompanyCommand command)
        {
            command.OrganisationId = _claims.GetOrganizationId();
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
    
    
    
}
