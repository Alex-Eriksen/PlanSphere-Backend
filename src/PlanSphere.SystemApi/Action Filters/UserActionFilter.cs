using Microsoft.AspNetCore.Mvc.Filters;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Action_Filters;

public class UserActionFilter(
    IUserRepository userRepository
) : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var activatingUserId = 0UL;
        try
        {
            activatingUserId = context.HttpContext.GetUserId();
        }
        catch
        {
            await next();
            return;
        }

        var userId = context.HttpContext.User.GetUserId();

        if (activatingUserId == userId)
        {
            await next();
            return;
        }

        var user = await _userRepository.GetByIdAsync(activatingUserId, CancellationToken.None);
        
        // user must have manage users for the user they are trying to access
        // We must then determine if the activated user belongs to something that the user also does.
        
        foreach (var role in user.Roles.Select(x => x.Role))
        {
            
        }
    }
}