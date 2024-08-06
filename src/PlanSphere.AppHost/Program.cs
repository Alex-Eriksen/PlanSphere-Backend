using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
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
        .WithHttpsEndpoint(targetPort: 5001)
        .WithHttpEndpoint(targetPort: 5000);
}

builder.Build().Run();