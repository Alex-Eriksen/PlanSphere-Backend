using AutoMapper;

namespace PlanSphere.Core.Utilities.Helpers.Mapper;

public static class MappingHelpers
{
    public static T? GetFromContext<T>(ResolutionContext context, string key) where T : class 
    {
        if (!context.TryGetItems(out var items) || !items.TryGetValue(key, out var obj) || obj is not T castedObj)
        {
            return null;
        }
        return castedObj;
    }
    
    public static T? GetEnumFromContext<T>(ResolutionContext context, string key) where T : struct, Enum
    {
        if (!context.TryGetItems(out var items) || !items.TryGetValue(key, out var obj) || !Enum.TryParse(typeof(T), obj.ToString(), out var result))
        {
            return null;
        }
        return (T)result!;
    }
    
    public static ulong? GetIdentifierFromContext(ResolutionContext context, string key)
    {
        if (!context.TryGetItems(out var items) || !items.TryGetValue(key, out var obj) || obj is not ulong identifier)
        {
            return null;
        }
        return identifier;
    }
    
    public static bool? GetBoolFromContext(ResolutionContext context, string key)
    {
        if (!context.TryGetItems(out var items) || !items.TryGetValue(key, out var obj) || !bool.TryParse(obj?.ToString(), out var result))
        {
            return null;
        }
        return result;
    }
}