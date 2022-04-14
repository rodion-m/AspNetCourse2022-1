using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class LogAsyncResourceFilter : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(
        ResourceExecutingContext context,
        ResourceExecutionDelegate next)
    {
        Console.WriteLine("Executing async!");
        context.Result = new OkResult(); //прерывание конвейера MVC
        ResourceExecutedContext executedContext = await next();
        Console.WriteLine("Executed async!");
    }
}