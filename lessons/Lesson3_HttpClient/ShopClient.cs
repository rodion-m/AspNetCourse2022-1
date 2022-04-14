using System.Net.Http.Json;
using System.Text.Json;
using Lesson4_HttpModels;

namespace Lesson3_HttpClient;

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
        var message = await _httpClient.PostAsJsonAsync(uri, product);
        var stream = await message.Content.ReadAsStreamAsync();
        var p = await JsonSerializer.DeserializeAsync<Product>(stream);
    }

    private async void Example()
    {
        var httpClient = new HttpClient();
        var uri = "http://site.com/api/order";
        var order = await httpClient.GetFromJsonAsync<Order>(uri);

        order.DeliveryAt = DateTime.Now;
        await httpClient.PostAsJsonAsync(uri, order);
    }
}