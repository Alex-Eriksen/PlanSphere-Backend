using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Extensions.APIExtensions;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Utilities.Helpers.JWT;
using PlanSphere.ServiceDefaults;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(withControllers: true);
builder.Services.AddSingleton<IJwtHelper, JwtHelper>();

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .Build(); 
});

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

builder.Services.AddSystemApiApplicationCore();
try
{
    var app = builder.Build();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors(Configurations.PlanSphereCors);

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
    
    app.UseAuthentication();
    app.UseAuthorization();

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