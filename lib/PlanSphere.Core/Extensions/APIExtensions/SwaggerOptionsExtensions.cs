using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PlanSphere.Core.Extensions.APIExtensions;

public static class SwaggerOptionsExtensions
{
    public static SwaggerGenOptions AddXMLComments(this SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        return options;
    }
}