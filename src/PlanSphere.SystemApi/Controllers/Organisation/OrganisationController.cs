using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers.Organisation;

public class OrganisationController(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost(Name = nameof(CreateOrganisationAsync))]
    public async Task<IActionResult> CreateOrganisationAsync(CreateOrganisationCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }
}