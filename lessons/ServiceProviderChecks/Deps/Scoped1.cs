namespace ServiceProviderChecks.Deps;

public class Scoped1
{
    public Scoped1(
        IServiceProvider serviceProvider,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<Scoped1> logger)
    {
        logger.LogWarning("ServiceProvider: {ServiceProvider}", serviceProvider.GetHashCode());
        logger.LogWarning("ServiceScopeFactory: {ServiceScopeFactory}", serviceScopeFactory.GetHashCode());
    }
}