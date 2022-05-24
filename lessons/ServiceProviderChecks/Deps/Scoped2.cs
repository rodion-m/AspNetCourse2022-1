namespace ServiceProviderChecks.Deps;

public class Scoped2
{
    public Scoped2(
        IServiceProvider serviceProvider,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<Scoped2> logger)
    {
        logger.LogWarning("ServiceProvider: {ServiceProvider}", serviceProvider.GetHashCode());
        logger.LogWarning("ServiceScopeFactory: {ServiceScopeFactory}", serviceScopeFactory.GetHashCode());
    }
}