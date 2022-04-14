using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class AddHeaderAttribute : ResultFilterAttribute
{
    private readonly string _header, _value;

    public AddHeaderAttribute(string header, string value)
    {
        (_header, _value) = (header, value);
    }
    
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add(_header, _value);
    }

    public override void OnResultExecuted(ResultExecutedContext _) { }
}