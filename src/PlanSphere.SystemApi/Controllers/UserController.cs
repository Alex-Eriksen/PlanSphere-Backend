using System.Security.Claims;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Features.Address.Requests;
using PlanSphere.Core.Features.Users.Commands.CreateUser;
using PlanSphere.Core.Features.Users.Commands.LoginUser;
using PlanSphere.Core.Features.Users.Queries.GetUserDetails;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.SystemApi.Action_Filters;
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
    
    [HttpPost(Name = nameof(CreateUserDevelopmentAsync))]
    public async Task<IActionResult> CreateUserDevelopmentAsync()
    {
        var request = new UserRequest()
        {
            Email = "test@test.com",
            FirstName = "Test",
            LastName = "Test",
            Address = new AddressRequest()
            {
                StreetName = null,
                HouseNumber = null,
                PostalCode = null,
                Door = null,
                Floor = null,
                CountryId = null
            }
        };
        var command = new CreateUserCommand(request, false)
        {
            OrganisationId = 1,
            UserId = 0
        };
        await _mediator.Send(command);
        return Created();
    }

    [HttpGet("{sourceLevel}/{sourceLevelId}/{userId?}", Name = nameof(GetUserDetailsAsync))]
    [TypeFilter(typeof(UserActionFilter))]
    // User must have ManageUser to access users that is not their own.
    // User 
    public async Task<IActionResult> GetUserDetailsAsync([FromRoute] SourceLevel sourceLevel, [FromRoute] ulong sourceLevelId, [FromRoute] ulong? userId)
    {
        var selectedUserId = userId ?? Request.HttpContext.User.GetUserId();
        var query = new GetUserDetailsQuery(selectedUserId)
        {
            SourceLevel = sourceLevel,
            SourceLevelId = sourceLevelId
        };
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [Authorize]
    [HttpGet(Name = nameof(Test))]
    public async Task<IActionResult> Test()
    {
        return NoContent();
    }
}