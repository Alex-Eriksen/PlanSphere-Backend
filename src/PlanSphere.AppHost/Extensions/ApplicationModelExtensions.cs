using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.Configuration;
using PlanSphere.Core.Constants;

namespace PlanSphere.AppHost.Extensions;

public static class ApplicationModelExtensions
{
    public static IResourceBuilder<ProjectResource> AddEnvironmentVariables(this IResourceBuilder<ProjectResource> builder, string environmentName, string environmentValue)
    {
        builder.WithEnvironment((context) =>
        {
            if (context.EnvironmentVariables.Any(env => env.Key == environmentName))
            {
                return;
            }

            context.EnvironmentVariables.Add(environmentName, environmentValue);
        });

        return builder;
    }
    
    public static IResourceBuilder<ProjectResource> AddAspNetCoreEnvironmentVariable(this IResourceBuilder<ProjectResource> builder)
    {
        builder.WithEnvironment((context) =>
        {
            if (context.EnvironmentVariables.Any(env =>
                    env.Key == AspireEnvironmentVariables.AspNetCoreEnvironmentVariableName))
            {
                context.EnvironmentVariables.Remove(AspireEnvironmentVariables.AspNetCoreEnvironmentVariableName);
            }

            if (context.EnvironmentVariables.Any(env =>
                    env.Key == AspireEnvironmentVariables.DotNetEnvironmentVariableName))
            {
                context.EnvironmentVariables.Remove(AspireEnvironmentVariables.DotNetEnvironmentVariableName);
            }

            context.EnvironmentVariables.Add(AspireEnvironmentVariables.AspNetCoreEnvironmentVariableName, builder.ApplicationBuilder.Environment.EnvironmentName);
            context.EnvironmentVariables.Add(AspireEnvironmentVariables.DotNetEnvironmentVariableName, builder.ApplicationBuilder.Environment.EnvironmentName);
        });
        return builder;
    }
    
    public static IResourceBuilder<ProjectResource> AddCustomEnvironmentVariables(this IResourceBuilder<ProjectResource> builder)
    {
        var envVars = builder.ApplicationBuilder.Configuration.GetSection($"CustomEnvironmentVariables:{builder.Resource.Name}").Get<Dictionary<string, string>>();

        if (envVars is not null && envVars.Any())
        {
            builder.WithEnvironment((context) =>
            {
                foreach (var variable in envVars)
                {
                    if (context.EnvironmentVariables.Any(env => env.Key == variable.Key)) continue;
                    context.EnvironmentVariables.Add(variable.Key, "{{{{ .Env." + variable.Value + " }}}}");
                }
            });
        }

        return builder;
    }
}