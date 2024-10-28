using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Roles.Commands.CreateRole;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

public class RoleRepository(IMediator mediator) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost(Name = nameof(CreateRoleAsync))]
    public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}