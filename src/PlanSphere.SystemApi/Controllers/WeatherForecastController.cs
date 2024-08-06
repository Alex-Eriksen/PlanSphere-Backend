using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.SystemApi.Controllers.Base;

namespace PlanSphere.SystemApi.Controllers;

public class WeatherForecastController(IMediator mediator) : ApiControllerBase(mediator)
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet(Name = nameof(GetWeatherForecastAsync))]
    public Task<IActionResult> GetWeatherForecastAsync()
    {
        return Task.FromResult<IActionResult>(Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray()));
    }
}