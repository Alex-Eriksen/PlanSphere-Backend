using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanSphere.Core.Extensions.APIExtensions;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Utilities.Helpers.JWT;
using PlanSphere.CronJobScheduler.Extensions;
using PlanSphere.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults(builder.Environment.IsDevelopment(), withControllers: false);
builder.Services.AddCronJobScheduler();
builder.Services.AddCronJobApplicationCore();
builder.Services.AddSingleton<IJwtHelper, JwtHelper>();

var host = builder.Build();
host.Run();
