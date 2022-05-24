namespace ServiceProviderChecks.Deps;

public class Transient2
{
    public Transient2(
        IServiceProvider serviceProvider,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<Transient2> logger)
    {
        logger.LogWarning("ServiceProvider: {ServiceProvider}", serviceProvider.GetHashCode());
        logger.LogWarning("ServiceScopeFactory: {ServiceScopeFactory}", serviceScopeFactory.GetHashCode());
    }
}