using System.Text;
using System.Text.Json;
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
    
    private static async Task<SourceLevel> GetSourceLevelAsync(this HttpContext context)
    {
        var jsonDoc = await context.GetRequestBodyAsJsonAsync();

        jsonDoc.RootElement.TryGetProperty(PermissionFilterConstants.SourceLevelKey, out var sourceLevelJson);

        Enum.TryParse<SourceLevel>(sourceLevelJson.ToString(), out var sourceLevel);
        
        return sourceLevel;
    }

    private static async Task<JsonDocument> GetRequestBodyAsJsonAsync(this HttpContext context)
    {
        context.Request.EnableBuffering();
        context.Request.Body.Position = 0;
        var body = string.Empty;

        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            body = await reader.ReadToEndAsync();
        }
        context.Request.Body.Position = 0;
        
        return JsonDocument.Parse(body);
    }
}