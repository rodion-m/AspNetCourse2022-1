using System.Net.Http.Json;
using Lesson04.HttpModels;

namespace Lesson03.HttpApiClient;

public class ShopClient
{
    private readonly string _host;
    private readonly HttpClient _httpClient;

    public ShopClient(string host, HttpClient? httpClient = null)
    {
        _host = host;
        _httpClient = httpClient ?? new HttpClient();
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
        await _httpClient.PostAsJsonAsync(uri, product);
    }
}