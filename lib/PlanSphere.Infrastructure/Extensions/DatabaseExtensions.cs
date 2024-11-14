using Microsoft.Extensions.DependencyInjection;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Infrastructure.Repositories;

namespace PlanSphere.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrganisationRepository, OrganisationRepository>();
        services.AddScoped<IJobTitleRepository, JobTitleRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IOrganisationRepository, OrganisationRepository>();
        services.AddScoped<IZipCodeRepository, ZipCodeRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IWorkTimeRepository, WorkTimeRepository>();
        services.AddScoped<IWorkTimeLogRepository, WorkTimeLogRepository>();
        return services;
    }
}