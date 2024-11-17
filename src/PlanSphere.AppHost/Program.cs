using PlanSphere.AppHost.Extensions;
using PlanSphere.Core.Constants;

var builder = DistributedApplication.CreateBuilder(args);

var isEnvLocal = builder.Configuration.GetSection("CUSTOM_ENVIRONMENT").Value == EnvironmentConstants.Local;

var systemApi = builder.AddProject<Projects.PlanSphere_SystemApi>(AspireComponents.SystemApi)
    .AddCustomEnvironmentVariables();

var scheduler = builder.AddProject<Projects.PlanSphere_CronJobScheduler>(AspireComponents.TaskScheduler)
    .AddCustomEnvironmentVariables();

var applications = new List<IResourceBuilder<ProjectResource>>();

applications.Add(systemApi);
applications.Add(scheduler);

if (isEnvLocal)
{
    applications.ForEach(app => app.AddAspNetCoreEnvironmentVariable());
}

if (!isEnvLocal)
{
    systemApi
        .WithHttpsEndpoint(port: 5001, name: AspireEndpoints.SystemApiHttps)
        .WithHttpEndpoint(port: 5000, name: AspireEndpoints.SystemApiHttp);
}

builder.Build().Run();