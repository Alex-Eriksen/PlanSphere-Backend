using System.Security.Claims;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
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

    if (!app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        });
    }

    app.MapGet("users/me", (ClaimsPrincipal claimsPrincipal) =>
    {
        return claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);
    }).RequireAuthorization();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    
    app.UseAuthorization();

    app.MapDefaultEndpoints();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}"
    );
    
    app.UseCors(Configurations.PlanSphereCors);
    
    app.Map("/", async context => { await context.Response.WriteAsync("Welcome to PlanSphere System API."); });

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