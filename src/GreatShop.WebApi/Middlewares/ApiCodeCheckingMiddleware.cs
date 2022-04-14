namespace GreatShop.WebApi.Middlewares
{
    public class ApiCodeCheckingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ApiCodeCheckingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers["Api-Key"].ToString() == "1111")
            {
                await _next(context); // passed = true;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden");
            }
        }

    }
}
