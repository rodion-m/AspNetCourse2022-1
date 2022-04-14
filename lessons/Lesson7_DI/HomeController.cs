using Microsoft.AspNetCore.Mvc;

namespace Lesson7_DI;

public class HomeController : ControllerBase
{
    private readonly LifeTimeTester _lifeTimeTester;
    private readonly ILogger<HomeController> _logger;
    private readonly SomeService _service;

    public HomeController(
        LifeTimeTester lifeTimeTester,
        SomeService service,
        ILogger<HomeController> logger)
    {
        _lifeTimeTester = lifeTimeTester;
        _service = service;
        _logger = logger;
    }

    public string Index()
    {
        _logger.LogInformation("FROM CONTROLLER: {Id}", _lifeTimeTester.Id);
        _service.DoWork();
        return "SEE LOGS";
    }
}