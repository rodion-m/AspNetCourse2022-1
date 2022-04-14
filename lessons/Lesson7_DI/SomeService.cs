namespace Lesson7_DI;

public class SomeService
{
    private readonly LifeTimeTester _lifeTimeTester;
    private readonly ILogger<SomeService> _logger;

    public SomeService(LifeTimeTester lifeTimeTester, ILogger<SomeService> logger)
    {
        _lifeTimeTester = lifeTimeTester;
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.LogInformation("FROM SERVICE: {Id}", _lifeTimeTester.Id);
    }
}