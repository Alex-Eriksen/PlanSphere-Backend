using Microsoft.AspNetCore.Mvc.Filters;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Exceptions;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.SystemApi.Action_Filters;

public class SystemAdministratorFilter(
    IUserRepository userRepository
) : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = context.HttpContext.User.GetUserId();

        var user = await _userRepository.GetByIdAsync(userId, CancellationToken.None);
        var userRoles = user.Roles.Select(x => x.Role);

        var isSystemAdmin = userRoles.Select(x => x.Name).Contains(PermissionFilterConstants.SystemAdministratorKey);
        
        if (!isSystemAdmin)
        {
            throw new UnauthorizedAccessException(ErrorMessageConstants.UnauthorizedActionMessage);
        }

        await next();
    }
}