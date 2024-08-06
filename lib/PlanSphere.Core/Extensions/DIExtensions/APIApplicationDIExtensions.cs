using System.Reflection;
using AutoMapper.EquivalencyExpression;
using Microsoft.Extensions.DependencyInjection;

namespace PlanSphere.Core.Extensions.DIExtensions;

public static class APIApplicationDIExtensions
{
    public static IServiceCollection AddAPIApplication(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection SetupAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddCollectionMappers();
            config.ShouldMapMethod = (m) =>
            {
                return m.ContainsGenericParameters && m.GetGenericMethodDefinition()
                    .GetGenericArguments()
                    .Any(i => i.GetGenericParameterConstraints().Length == 0);
            };
        }, Assembly.GetExecutingAssembly());
        return services;
    }
}