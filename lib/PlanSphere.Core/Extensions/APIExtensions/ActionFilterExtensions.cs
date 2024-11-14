using Microsoft.Extensions.DependencyInjection;
using PlanSphere.Core.Filters.LateFilters;
using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;

namespace PlanSphere.Core.Extensions.APIExtensions;

public static class ActionFilterExtensions
{
    public static IServiceCollection AddLateFilters(this IServiceCollection services)
    {
        services.AddScoped<IRoleFilter, RoleFilter>();
        services.AddScoped<IWorkScheduleFilter, WorkScheduleFilter>();
        services.AddScoped<IOrganisationFilter, OrganisationFilter>();

        return services;
    }
}