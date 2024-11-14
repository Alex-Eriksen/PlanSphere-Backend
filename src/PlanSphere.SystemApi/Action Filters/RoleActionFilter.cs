using Domain.Entities.EmbeddedEntities;
using Microsoft.AspNetCore.Mvc.Filters;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.SystemApi.Action_Filters;

public class RoleActionFilter(
    IRoleFilter roleFilter,
    IOrganisationFilter organisationFilter,
    Right requiredRight,
    SourceLevel? targetedLayer = null,
    bool isSpecific = false
) : ActionFilterAttribute
{
    private readonly IRoleFilter _roleFilter = roleFilter ?? throw new ArgumentNullException(nameof(roleFilter));
    private readonly IOrganisationFilter _organisationFilter = organisationFilter ?? throw new ArgumentNullException(nameof(organisationFilter));

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (await _organisationFilter.CheckIsOrganisationOwnerAsync(context.HttpContext.User.GetOrganisationId(), context.HttpContext.User.GetUserId(), CancellationToken.None, false))
        {
            await next();
            return;
        }
        await _roleFilter.CheckIsAuthorizedSourceLevelAsync(context.HttpContext, requiredRight, targetedLayer, isSpecific);

        await next();
    }
}