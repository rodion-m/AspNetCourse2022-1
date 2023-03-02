using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class AppExceptionFilter2 : Attribute, IExceptionFilter, IOrderedFilter
{
    public int Order => int.MinValue;

    public void OnException(ExceptionContext context)
    {
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