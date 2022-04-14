using Microsoft.AspNetCore.Mvc;

namespace Lesson23.Filters.Middlewares;

public class UnauthorizedResponseModelMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ResponseDefaultFormatterService _formatterService;

    public UnauthorizedResponseModelMiddleware(
        RequestDelegate next,
        ResponseDefaultFormatterService formatterService)
    {
        _next = next;
        _formatterService = formatterService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if(context.Response.HasStarted) return;
        
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            var result = new UnauthorizedObjectResult(new { Message = "Unauthorized" });
            await _formatterService.WriteAsync(context, result);
        }
    }
}