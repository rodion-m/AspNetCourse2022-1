using System.Net.Http.Json;
using Lesson04.HttpModels;

namespace Lesson03.HttpApiClient;

//Тип проекта: Class Library

/// <summary>
/// API-клиент нашего сервиса
/// </summary>
public class ShopClient : IShopClient
{
    private const string DefaultHost = "https://api.mysite.com";
    private readonly string _host;
    private readonly HttpClient _httpClient;
    
    public ShopClient(string host = DefaultHost, HttpClient? httpClient = null)
    {
        _host = host;
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        string uri = $"{_host}/products";
        IReadOnlyList<Product>? response = await _httpClient.GetFromJsonAsync<IReadOnlyList<Product>>(uri);
        return response!;
    }

    public async Task AddProduct(Product product)
    {
        if (product is not null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        var uri = $"{_host}/add_product";
        await _httpClient.PostAsJsonAsync(uri, product);
    }
}