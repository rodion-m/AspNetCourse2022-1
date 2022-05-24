namespace ServiceProviderChecks.Deps;

public class Singleton1
{
    public Singleton1(
        IServiceProvider serviceProvider,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<Singleton1> logger)
    {
        //serviceProvider.GetRequiredService<Scoped3>(); //throws InvalidOperationException because serviceProvider here is a root provider.
        logger.LogWarning("ServiceProvider: {ServiceProvider}", serviceProvider.GetHashCode());
        logger.LogWarning("ServiceScopeFactory: {ServiceScopeFactory}", serviceScopeFactory.GetHashCode());
    }
}