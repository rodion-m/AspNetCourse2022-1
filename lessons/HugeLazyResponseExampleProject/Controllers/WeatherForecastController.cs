using Microsoft.AspNetCore.Mvc;

namespace HugeLazyResponseExampleProject.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task Get()
    {
        // Откройте консоль разработчика, запустите этот запрос и вы увидите как каждую секунду загружаются новые данные.
        // Это JSON лениво записывается в поток ответа
        const int total = 60_000;
        var forecasts = Enumerable.Range(1, total).Select(index =>
        {
            if (index % 1000 == 0)
            {
                // После записи каждой 1000 элементов ждем 1 секунду
                _logger.LogInformation("Записано {Current} из {Total} элементов", index, total);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        });
        
        await HttpContext.Response.WriteAsJsonAsync(forecasts);
    }
}