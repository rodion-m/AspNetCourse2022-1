namespace ServiceProviderChecks.Deps;

public class Transient3
{
    public Transient3(
        IServiceProvider serviceProvider,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<Transient3> logger)
    {
        logger.LogWarning("ServiceProvider: {ServiceProvider}", serviceProvider.GetHashCode());
        logger.LogWarning("ServiceScopeFactory: {ServiceScopeFactory}", serviceScopeFactory.GetHashCode());
    }
}