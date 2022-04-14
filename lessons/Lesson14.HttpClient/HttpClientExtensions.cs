using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;

namespace Lesson14.HttpClient;

internal static class HttpClientExtensions
{
    internal static async Task<TResponse?> PostAsJsonAsync<TRequest, TResponse>(
        this System.Net.Http.HttpClient client, string? requestUri, TRequest request)
    {
        using var response = await client.PostAsJsonAsync(requestUri, request);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TResponse>();

        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new ShopUnauthorizedAccessException();

        if (response.Content.Headers.ContentType?.MediaType == MediaTypeNames.Application.Json)
        {
            var details = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            throw new ShopApiException(response.StatusCode, details);
        }

        var message = await response.Content.ReadAsStringAsync();
        throw new ShopApiException(response.StatusCode, message ?? "Unknown error");
    }
}