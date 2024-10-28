using Microsoft.Extensions.DependencyInjection;
using PlanSphere.Infrastructure.Contexts;

namespace PlanSphere.Infrastructure.DBExtensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<PlanSphereDatabaseContext>(options => { });
        return services;
    }
}