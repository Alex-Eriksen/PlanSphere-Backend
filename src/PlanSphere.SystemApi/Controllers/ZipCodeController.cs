using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.ZipCodes.Queries.GetZipCodeLookUps;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

public class ZipCodeController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet(Name = nameof(GetZipCodeLookUpsAsync))]
    [Authorize]
    public async Task<IActionResult> GetZipCodeLookUpsAsync()
    {
        var query = new GetZipCodeLookUpsQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}