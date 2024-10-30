using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Users.Commands.CreateUser;
using PlanSphere.Core.Features.Users.Commands.LoginUser;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

public class UserController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext?.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    
    [HttpPost(Name = nameof(CreateUserAsync))]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
    {
        command.UserId = _claims.GetUserId();
        command.OrganisationId = _claims.GetOrganizationId();
        await _mediator.Send(command);
        return Created();
    }
    
    [HttpPost(Name = nameof(LoginAsync))]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand userCommand)
    {
        var token = await _mediator.Send(userCommand);
        return Ok(token);
    }
}