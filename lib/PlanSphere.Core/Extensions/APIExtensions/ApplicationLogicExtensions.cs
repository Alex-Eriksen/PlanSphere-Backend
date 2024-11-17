using System.Reflection;
using FluentValidation;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Utilities.Options.JWT;
using PlanSphere.Core.Validation;

namespace PlanSphere.Core.Extensions.APIExtensions;

public static class ApplicationLogicExtensions
{
    public static IServiceCollection AddSystemApiApplicationCore(this IServiceCollection services)
    {
        AddMediatR(services, [HandlerType.SystemApi, HandlerType.Global]);
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = services.BuildServiceProvider()
                    .GetRequiredService<IOptions<JwtBearerOptions>>()
                    .Value;

                options.TokenValidationParameters = jwtOptions.TokenValidationParameters;
            });
        
        services.AddSwaggerSystemApi();
        return services;
    } 
    
    private static void AddMediatR(IServiceCollection services, IList<HandlerType> handlerTypes)
    {
        services.AddMediatR((config) =>
        {
            config.AddApiPostProcessors(handlerTypes);
            config.AddApiPreProcessors(handlerTypes);
            config.TypeEvaluator = (type) =>
            {
                return type.GetCustomAttributes()
                    .Any(attribute => attribute is HandlerTypeAttribute a && handlerTypes.Contains(a.Value));
            };
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private static void AddApiPostProcessors(this MediatRServiceConfiguration configuration, IList<HandlerType> handlerTypes)
    {
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.GetInterfaces()
                               .Any(interfaceType => interfaceType.IsGenericType
                                                     && interfaceType.GetGenericTypeDefinition() == typeof(IRequestPostProcessor<,>))
                           && !type.IsInterface
                           && !type.IsAbstract
                           && type.GetCustomAttributes()
                               .Any(attribute => attribute is HandlerTypeAttribute a && (handlerTypes.Contains(a.Value))))
            .ToList();

        foreach (var type in types)
        {
            configuration.AddRequestPostProcessor(type);
        }
    }
    
    public static IServiceCollection AddCronJobApplicationCore(this IServiceCollection services)
    {
        AddMediatR(services, [HandlerType.TaskScheduler, HandlerType.Global]);

        return services;
    }


    private static void AddApiPreProcessors(this MediatRServiceConfiguration configuration, IList<HandlerType> handlerTypes)
    {
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.GetInterfaces()
                               .Any(interfaceType => interfaceType.IsGenericType
                                                     && interfaceType.GetGenericTypeDefinition() == typeof(IRequestPreProcessor<>))
                           && !type.IsInterface
                           && !type.IsAbstract
                           && type.GetCustomAttributes()
                               .Any(attribute => attribute is HandlerTypeAttribute a && (handlerTypes.Contains(a.Value))))
            .ToList();

        foreach (var type in types)
        {
            configuration.AddRequestPreProcessor(type);
        }
    }

    private static void AddSwaggerSystemApi(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}