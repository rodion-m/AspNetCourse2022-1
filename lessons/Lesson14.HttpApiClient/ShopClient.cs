using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Lesson14.Models;
using Lesson14.Models.Requests;
using Lesson14.Models.Responses;

namespace Lesson14.HttpApiClient;

//SDK
public class ShopClient : IDisposable
{
    private readonly HttpMessageHandler _handler;
    private readonly string _host;
    private readonly HttpClient _httpClient;

    public ShopClient(string host, HttpMessageHandler? handler = null)
    {
        _host = host;
        _handler = handler ?? new HttpClientHandler();
        _httpClient = new HttpClient(_handler);
    }

    public bool IsAuthorizationTokenSet { get; private set; }

    public void SetAuthorizationToken(string token)
    {
        if (token == null) throw new ArgumentNullException(nameof(token));
        var header = new AuthenticationHeaderValue("Bearer", token);
        _httpClient.DefaultRequestHeaders.Authorization = header;
        IsAuthorizationTokenSet = true;
    }

    public void ResetAuthorizationToken()
    {
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
        IsAuthorizationTokenSet = false;
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync<RegisterRequest, RegisterResponse>(
            $"{_host}/auth/register", request);

        SetAuthorizationToken(response!.Token);

        return response;
    }

    public async Task<LogInResponse> LogIn(LogInRequest request)
    {
        LogInResponse? response = await _httpClient.PostAsJsonAsync<LogInRequest, LogInResponse>(
            $"{_host}/auth/login", request);
        
        SetAuthorizationToken(response!.Token);
        
        return response;
    }

    public Task<Account> GetAccount()
    {
        return _httpClient.GetFromJsonAsync<Account>($"{_host}/auth/get_account")!;
    }

    public async Task<Cart> GetCart()
    {
        var uri = $"{_host}/cart/get";
        var response = await _httpClient.GetFromJsonAsync<Cart>(uri);
        return response!;
    }

    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        var uri = $"{_host}/products";
        var response = await _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>(uri);
        return response!;
    }

    public async Task AddProduct(Product product)
    {
        var uri = $"{_host}/add_product";
        using var message = await _httpClient.PostAsJsonAsync(uri, product);
        await using var stream = await message.Content.ReadAsStreamAsync();
        var p = await JsonSerializer.DeserializeAsync<Product>(stream);
    }
    
    public void Dispose()
    {
        _httpClient.Dispose();
    }
}