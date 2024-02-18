using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class CentralizedExceptionHandlingFilter 
    : Attribute, IExceptionFilter, IOrderedFilter
{
    public CentralizedExceptionHandlingFilter()
    {
        ; //на каждый запрос создается новый экземпляр (но не из DI)
    }
    
    public int Order { get; set; }

    public void OnException(ExceptionContext context)
    {
        if (TryGetUserMessageFromException(context, out var message))
        {
            int statusCode = StatusCodes.Status400BadRequest;
            context.Result = new ObjectResult(new ErrorResponse(statusCode, message!))
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }

    private bool TryGetUserMessageFromException(ExceptionContext context, out string? message)
    {
        message = context.Exception switch
        {
            EmailNotFoundException => "Аккаунт с таким Email не найден",
            IncorrectPasswordException => "Неверный пароль",
            _ => null
        };
        return message != null;
    }
    
}