using System.Security.Claims;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Extensions.APIExtensions;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Utilities.Helpers.JWT;
using PlanSphere.Core.Utilities.Options.JWT;
using PlanSphere.ServiceDefaults;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(withControllers: true);
builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.AddSingleton<IJwtHelper, JwtHelper>();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .Build(); 
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field. Example: \"Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
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


builder.Services.AddSystemApiApplicationCore();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Configurations.PlanSphereCors,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
try
{
    var app = builder.Build();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors(Configurations.PlanSphereCors); // Apply CORS policy before Authentication/Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });

    app.MapControllers();
    app.MapDefaultEndpoints();
    app.Map("/", async context => { await context.Response.WriteAsync("Welcome to PlanSphere System API."); });
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}"
    );
    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}