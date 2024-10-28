using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace PlanSphere.Core.Extensions.APIExtensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(o =>
        {
            o.AddXMLComments();
            
            o.AddSecurityDefinition("KeyCloak",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["KeyCloakConfiguration:AuthorizationUrl"]!),
                            TokenUrl = new Uri(configuration["KeyCloakConfiguration:TokenUrl"]!)
                        }
                    }
                });

            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "KeyCloak",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                    {
                        "openid"
                    }
                }
            });

        });

        return services;
    }
}