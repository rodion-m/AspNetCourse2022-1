namespace Lesson18.Middleware.Middlewares;

public class ResponseLoggingMiddleware
{
    private readonly ILogger<ResponseLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger")
            || context.Request.ContentType == "application/grpc")
        {
            await _next(context);
            return;
        }

        var response = context.Response;
        var originalBody = response.Body;
        await using var responseBodyReplacement = new MemoryStream();
        response.Body = responseBodyReplacement;

        await _next(context);

        try
        {
            responseBodyReplacement.Seek(0, SeekOrigin.Begin);
            await responseBodyReplacement.CopyToAsync(originalBody);

            responseBodyReplacement.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBodyReplacement).ReadToEndAsync();
            LogResponseHeaders(response);
            LogResponseBody(response, responseText);
        }
        finally
        {
            response.Body = originalBody;
        }
    }

    private void LogResponseBody(HttpResponse response, string? responseText)
    {
        _logger.LogInformation(
            "Response {StatusCode}: {@Response}", response.StatusCode, responseText);
    }

    private void LogResponseHeaders(HttpResponse response)
    {
        _logger.LogInformation("[Response Headers]: {@Headers}", response.Headers);
    }
}