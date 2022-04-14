using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class AppExceptionFilter1 : Attribute, IExceptionFilter, IOrderedFilter
{
    public int Order { get; set; }

    public AppExceptionFilter1()
    {
    }
    
    public void OnException(ExceptionContext context)
    {
        var message = TryGetUserMessageFromException(context);
        if (message != null)
        {
            context.Result = new ObjectResult(new ErrorModel(message));
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

public class IncorrectPasswordException : Exception
{
}

public class EmailNotFoundException : Exception
{
    public EmailNotFoundException(string email)
    {
        Email = email;
    }

    public string Email { get; }
}