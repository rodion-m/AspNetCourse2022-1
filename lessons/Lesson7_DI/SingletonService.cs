using Lesson7_DI;

public class SingletonService
{
    public SingletonService(LifeTimeTester tester, ILogger<SingletonService> logger)
    {
        logger.LogInformation("FROM SINGLETON: {Id}", tester.Id);
    }

    // public SingletonService(
    //     IServiceProvider serviceProvider, ILogger<SingletonService> logger)
    // {
    //     using (IServiceScope serviceScope = serviceProvider.CreateScope())
    //     {
    //         var lifeTimeTester = serviceScope.ServiceProvider.GetService<LifeTimeTester>()!;
    //         logger.LogInformation("FROM SINGLETON: {Id}", lifeTimeTester.Id);
    //     }
    // }
}