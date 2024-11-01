using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PlanSphere.Core.Filters;
using PlanSphere.Core.Json;
using PlanSphere.Core.Utilities.Formatter;

namespace PlanSphere.Core.Extensions.APIExtensions;

public static class ControllerExtensions
{
    public static IHostApplicationBuilder SetupControllers(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGenWithAuth(builder.Configuration);
        builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProduceResponseTypeModelProvider>());
        builder.Services.AddControllers(options =>
            {
                options.AddSwaggerErrorResponses();
                options.Filters.Add<ExceptionActionFilter>();
                options.InputFormatters.Insert(0, NewtonsoftInputFormatter.GetJsonPatchInputFormatter());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        builder.Services.AddEndpointsApiExplorer();
        return builder;
    }
}