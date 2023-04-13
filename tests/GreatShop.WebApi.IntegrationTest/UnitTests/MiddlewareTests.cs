using System.IO;
using System.Threading.Tasks;
using GreatShop.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace GreatShop.WebApi.IntegrationTest.UnitTests;

public class MiddlewareTests
{
    [Fact]
    public void _2_plus_2_is_4()
    {
        Assert.Equal(4, 2 + 2);
    }

    [Fact]
    public async Task ApiCodeCheckingMiddleware_with_correct_code_ALLOWS_access_to_the_service()
    {
        var passed = false;
        var middleware = new ApiCodeCheckingMiddleware(_ =>
        {
            passed = true;
            return Task.CompletedTask;
        });

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers.Add("Api-Key", "1111");
        
        await middleware.InvokeAsync(httpContext);
        
        Assert.True(passed);
    }
    
    [Fact]
    public async Task ApiCodeCheckingMiddleware_without_code_DENIES_access()
    {
        var passed = false;
        var middleware = new ApiCodeCheckingMiddleware(async _ =>
        {
            passed = true;
        });

        var httpContext = new DefaultHttpContext();
        using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;
        
        await middleware.InvokeAsync(httpContext);
        
        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        
        var sr = new StreamReader(httpContext.Response.Body);
        string response = await sr.ReadToEndAsync();

        Assert.False(passed);
        Assert.Equal(httpContext.Response.StatusCode, StatusCodes.Status403Forbidden);
        Assert.Equal("Forbidden", response);
    }
}