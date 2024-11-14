using System.Security.Claims;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Features.Addresses.Requests;
using PlanSphere.Core.Features.JobTitles.Commands.AssignJobTitle;
using PlanSphere.Core.Features.Roles.Commands.AssignRole;
using PlanSphere.Core.Features.Users.Commands.CreateUser;
using PlanSphere.Core.Features.Users.Commands.LoginUser;
using PlanSphere.Core.Features.Users.Commands.PatchUser;
using PlanSphere.Core.Features.Users.Queries.GetUserDetails;
using PlanSphere.Core.Features.Users.Queries.LookUpUsers;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

[Authorize]
public class UserController(IMediator mediator, IRoleFilter roleFilter) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IRoleFilter _roleFilter = roleFilter ?? throw new ArgumentNullException(nameof(roleFilter));

    [HttpPost(Name = nameof(CreateUserAsync))]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
    {
        command.UserId = Request.HttpContext.User.GetUserId();
        command.OrganisationId = Request.HttpContext.User.GetOrganisationId();
        await _mediator.Send(command);
        return Created();
    }
    
    [AllowAnonymous]
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

    [HttpGet("{userId?}", Name = nameof(GetUserDetailsAsync))]
    [TypeFilter(typeof(UserActionFilter))]
    public async Task<IActionResult> GetUserDetailsAsync([FromRoute] ulong? userId)
    {
        var selectedUserId = userId ?? Request.HttpContext.User.GetUserId();
        var query = new GetUserDetailsQuery(selectedUserId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPatch("{userId?}", Name = nameof(PatchUserAsync))]
    [TypeFilter(typeof(UserActionFilter))]
    public async Task<IActionResult> PatchUserAsync([FromRoute] ulong? userId, [FromBody] JsonPatchDocument<UserPatchRequest> request)
    {
        var selectedUserId = userId ?? Request.HttpContext.User.GetUserId();
        var command = new PatchUserCommand(selectedUserId, request);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{roleId}", Name = nameof(AssignRoleToSelfAsync))]
    [TypeFilter(typeof(UserActionFilter))]
    public async Task<IActionResult> AssignRoleToSelfAsync(ulong roleId)
    {
        await _roleFilter.CheckIsAllowedToSetOwnRolesAsync(Request.HttpContext);
        var command = new AssignRoleCommand(roleId, Request.HttpContext.User.GetUserId());
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{organisationId?}", Name = nameof(LookUpUsersAsync))]
    public async Task<IActionResult> LookUpUsersAsync(ulong? organisationId)
    {
        var selectedId = organisationId ?? Request.HttpContext.User.GetOrganisationId();
        var query = new LookUpUsersQuery(selectedId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost("{jobTitleId}", Name = nameof(AssignJobTitleToSelfAsync))]
    [TypeFilter(typeof(UserActionFilter))]
    public async Task<IActionResult> AssignJobTitleToSelfAsync(ulong jobTitleId)
    {
        await _roleFilter.CheckIsAllowedToSetOwnJobTitlesAsync(Request.HttpContext);
        var command = new AssignJobTitleCommand(jobTitleId, Request.HttpContext.User.GetUserId());
        await _mediator.Send(command);
        return NoContent();
    }
}