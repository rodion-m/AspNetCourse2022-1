using System.Diagnostics;

namespace Lesson7_Mail.Services;

public class ExampleBackgroundService : BackgroundService
{
    private readonly ILogger<ExampleBackgroundService> _logger;

    public ExampleBackgroundService(ILogger<ExampleBackgroundService> logger)
    {
        _logger = logger; //DI работает
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        var sw = Stopwatch.StartNew();
        while (await timer.WaitForNextTickAsync(stoppingToken))
            _logger.LogInformation("Сервер работает уже {WorkTime}", sw.Elapsed);
    }
}