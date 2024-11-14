using Domain.Entities.EmbeddedEntities;
using Microsoft.AspNetCore.Http;

namespace PlanSphere.Core.Interfaces.ActionFilters.LateFilters;

public interface IRoleFilter
{
    Task CheckIsAuthorizedSourceLevelAsync(HttpContext context, Right requiredRight, SourceLevel? targetedLayer = null, bool isSpecific = false);
    Task CheckIsAllowedToSetOwnRolesAsync(HttpContext context);
    Task CheckIsAllowedToSetOwnJobTitlesAsync(HttpContext context);
}