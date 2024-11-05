using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Organisations.Queries.LookUp;
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
}