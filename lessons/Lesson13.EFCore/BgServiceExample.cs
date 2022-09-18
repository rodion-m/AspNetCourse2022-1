using Lesson13.EFCore.Data;

namespace Lesson13.EFCore;

public class BgServiceExample : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public BgServiceExample(IServiceProvider serviceProvider, AppDbContext dbContext)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var serviceScope = _serviceProvider.CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;
        var dbContext = provider.GetRequiredService<AppDbContext>();
        var orders = dbContext.Orders.ToList();
        //...
    }
}