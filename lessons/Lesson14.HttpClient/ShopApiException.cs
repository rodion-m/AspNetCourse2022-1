using System.Net;

namespace Lesson14.HttpClient;

public class ShopApiException : Exception
{
    public ShopApiException(HttpStatusCode statusCode, ValidationProblemDetails details)
        : base(details.Title)
    {
        StatusCode = statusCode;
        Details = details;
    }

    public ShopApiException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
    public ValidationProblemDetails? Details { get; }
}