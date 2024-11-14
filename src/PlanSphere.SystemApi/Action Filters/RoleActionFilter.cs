using Domain.Entities.EmbeddedEntities;
using Microsoft.AspNetCore.Mvc.Filters;
using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.SystemApi.Action_Filters;

public class RoleActionFilter(
    IRoleFilter roleFilter,
    Right requiredRight,
    SourceLevel? targetedLayer = null,
    bool isSpecific = false
) : ActionFilterAttribute
{
    private readonly IRoleFilter _roleFilter = roleFilter ?? throw new ArgumentNullException(nameof(roleFilter));

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await _roleFilter.CheckIsAuthorizedSourceLevelAsync(context.HttpContext, requiredRight, targetedLayer, isSpecific);

        await next();
    }
}