using System.Net.Http.Json;


var httpClient = new HttpClient() {BaseAddress = new Uri("https://localhost:7166")};
await httpClient.GetAsync("/products/clear"); //Очищаем список товаров
await Parallel.ForEachAsync(Product.GenerateProducts(1000), async (product, _) =>
{
    //await httpClient.PostAsJsonAsync("/catalog/add", product);
    await httpClient.PostAsJsonAsync("/catalog/add_with_lock", product);
    Console.WriteLine($"{product.Name} added");
});

var result = await httpClient.GetFromJsonAsync<Product[]>("/products"); //"null"
Console.WriteLine("-----------------------------------");
Console.WriteLine(result!.Length);


record Product(string Name, double Price)
{
    public static IEnumerable<Product> GenerateProducts(int count)
    {
        for (var i = 0; i < count; i++)
        {
            yield return new Product($"Product {i}", 1000);
        }
    }
}

