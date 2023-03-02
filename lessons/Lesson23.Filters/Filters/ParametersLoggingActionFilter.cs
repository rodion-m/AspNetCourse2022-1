using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class ParametersLoggingActionFilter : IActionFilter
{
    private readonly ILogger<ParametersLoggingActionFilter> _logger;

    public ParametersLoggingActionFilter(ILogger<ParametersLoggingActionFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // Выполняется перед методом действия
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        foreach (var (key, value) in context.ActionArguments)
        {
            _logger.LogInformation("[{Endpoint}] {Param}: {@Value}",
                context.ActionDescriptor.DisplayName, key, value);
        }
    }

    // Выполняется после метода действия
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var actionResult = context.Result;
        if (actionResult is ObjectResult objectResult)
        {
            _logger.LogInformation("{@ObjectResult}", objectResult.Value);
        }
    }
}

public class MyRequestModel
{
}