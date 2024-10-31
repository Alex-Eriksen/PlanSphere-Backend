using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Features.Users.Commands.LoginUser;
using PlanSphere.Core.Features.Users.Commands.RefreshToken;
using PlanSphere.Core.Features.Users.Commands.RevokeRefreshToken;
using PlanSphere.Core.Features.Users.Queries.GetLoggedInUser;
using PlanSphere.SystemApi.Controllers.Base;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Controllers;

public class AuthenticationController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ApiControllerBase(mediator)
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ClaimsPrincipal _claims = httpContextAccessor.HttpContext?.User ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    
    [HttpPost(Name = nameof(LoginAsync))]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand userCommand)
    {
        userCommand.IpAddress = IpAddress();
        var refreshTokenDto = await _mediator.Send(userCommand);
        SetTokenCookie(refreshTokenDto.RefreshToken);
        
        return Ok(refreshTokenDto.AccessToken);
    }

    [HttpPost(Name = nameof(RefreshTokenAsync))]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        var refreshToken = Request.Cookies[HttpContextConstants.RefreshToken];
        var command = new RefreshTokenCommand(refreshToken, IpAddress());
        var refreshTokenDto = await _mediator.Send(command);
        SetTokenCookie(refreshTokenDto.RefreshToken);
        
        return Ok(refreshTokenDto.AccessToken);
    }

    [Authorize]
    [HttpPost(Name = nameof(RevokeRefreshTokenAsync))]
    public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] string? refreshToken)
    {
        var token = refreshToken ?? Request.Cookies[HttpContextConstants.RefreshToken];
        var command = new RevokeRefreshTokenCommand(token, IpAddress());
        
        await _mediator.Send(command);
        Response.Cookies.Delete(HttpContextConstants.RefreshToken);
        
        return NoContent();
    }

    [Authorize]
    [HttpGet(Name = nameof(GetLoggedInUserAsync))]
    public async Task<IActionResult> GetLoggedInUserAsync()
    {
        var refreshToken = Request.Cookies[HttpContextConstants.RefreshToken];
        var accessToken = Request.Headers.Authorization.ToString().Replace("bearer ", "");
        var command = new GetLoggedInUserQuery(refreshToken, _claims.GetUserId());
        var loggedInUserDto = await _mediator.Send(command);
        
        return Ok(loggedInUserDto);
    }
    
    private void SetTokenCookie(string token)
    {
        CookieOptions options = new()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        };

        Response.Cookies.Append(HttpContextConstants.RefreshToken, token, options);
    }
    
    private string IpAddress()
    {
        if (Request.Headers.TryGetValue("X-Forwarded-For", out var value))
        {
            return value!;
        }
        else
        {
            return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        }
    }
}