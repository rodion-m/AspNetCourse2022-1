using Microsoft.AspNetCore.Mvc;

namespace ServiceProviderChecks;

public class SomeController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public SomeController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IActionResult SomeAction()
    {
        //Resolve a scoped service from IServiceProvider without creating a scope:
        var service = _serviceProvider.GetRequiredService<ScopedService>();
        return Ok(service.GetHashCode());
    }
}

public class ScopedService
{
}