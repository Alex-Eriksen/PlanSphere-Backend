﻿using System.Security.Claims;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Features.Addresses.Requests;
using PlanSphere.Core.Features.Users.Commands.CreateUser;
using PlanSphere.Core.Features.Users.Commands.LoginUser;
using PlanSphere.Core.Features.Users.Commands.PatchUser;
using PlanSphere.Core.Features.Users.Queries.GetUserDetails;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.SystemApi.Action_Filters;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

public class UserController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext?.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    
    [HttpPost(Name = nameof(CreateUserAsync))]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
    {
        command.UserId = _claims.GetUserId();
        command.OrganisationId = _claims.GetOrganisationId();
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
    
    [Authorize]
    [HttpGet(Name = nameof(Test))]
    public async Task<IActionResult> Test()
    {
        return NoContent();
    }
}