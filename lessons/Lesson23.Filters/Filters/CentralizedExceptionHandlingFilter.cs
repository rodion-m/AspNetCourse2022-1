using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class CentralizedExceptionHandlingFilter 
    : Attribute, IExceptionFilter, IOrderedFilter
{
    public int Order { get; set; }

    public void OnException(ExceptionContext context)
    {
        var message = TryGetUserMessageFromException(context);
        int statusCode = StatusCodes.Status400BadRequest;
        if (message != null)
        {
            context.Result = new ObjectResult(new ErrorResponse(statusCode, message))
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }

    private string? TryGetUserMessageFromException(ExceptionContext context)
    {
        return context.Exception switch
        {
            EmailNotFoundException => "Аккаунт с таким Email не найден",
            IncorrectPasswordException => "Неверный пароль",
            _ => null
        };
    }
    
}