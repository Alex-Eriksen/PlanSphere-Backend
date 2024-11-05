using System.Text;
using System.Text.Json;
using Domain.Entities.EmbeddedEntities;
using Microsoft.AspNetCore.Http;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Extensions.HttpContextExtensions;

public static class HttpContextExtensions
{
    public static ulong GetSourceLevelId(this HttpContext context)
    {
        context.Request.RouteValues.TryGetValue(PermissionFilterConstants.SourceLevelIdKey, out var sourceLevelIdValue);
        ulong.TryParse(sourceLevelIdValue?.ToString(), out var sourceLevelId);
        
        return sourceLevelId;
    }

    public static SourceLevel GetSourceLevel(this HttpContext context)
    {
        context.Request.RouteValues.TryGetValue(PermissionFilterConstants.SourceLevelKey, out var sourceLevelValue);
        Enum.TryParse<SourceLevel>(sourceLevelValue.ToString(), out var sourceLevel);

        return sourceLevel;
    }
}