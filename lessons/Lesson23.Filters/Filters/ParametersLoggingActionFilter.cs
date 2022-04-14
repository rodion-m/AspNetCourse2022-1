using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class ParametersLoggingActionFilter : IActionFilter
{
    private readonly ILogger<ParametersLoggingActionFilter> _logger;

    public ParametersLoggingActionFilter(ILogger<ParametersLoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var (key, value) in context.ActionArguments)
        {
            _logger.LogInformation("[{Endpoint}] {Param}: {@Value}",
                context.ActionDescriptor.DisplayName, key, value);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}

public class MyRequestModel
{
}