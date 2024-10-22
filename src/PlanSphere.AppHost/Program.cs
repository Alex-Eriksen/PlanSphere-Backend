using PlanSphere.AppHost.Extensions;
using PlanSphere.Core.Constants;

var builder = DistributedApplication.CreateBuilder(args);

var isEnvLocal = builder.Configuration.GetSection("CUSTOM_ENVIRONMENT").Value == EnvironmentConstants.Local;

var systemApi = builder.AddProject<Projects.PlanSphere_SystemApi>(AspireComponents.SystemApi)
    .AddCustomEnvironmentVariables();

var applications = new List<IResourceBuilder<ProjectResource>>();

applications.Add(systemApi);

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