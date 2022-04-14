using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class LogResourceFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        // if (context.HttpContext.Request.ContentType != "application/json")
        // {
        //     context.Result = new BadRequestObjectResult("Формат данных не поддерживается");
        // }
        
        Console.WriteLine("Executing!");
    }
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine("Executed");
    }
}