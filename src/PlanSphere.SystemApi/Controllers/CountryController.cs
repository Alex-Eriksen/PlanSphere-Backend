using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Countries.Queries.GetCountries;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

public class CountryController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet(Name = nameof(GetCountryLookUpsAsync))]
    public async Task<IActionResult> GetCountryLookUpsAsync()
    {
        var query = new GetCountryLookUpsQuery();
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}
