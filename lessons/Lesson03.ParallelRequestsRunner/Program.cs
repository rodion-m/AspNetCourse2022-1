using System.Net.Http.Json;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
// Пушка для тестирования потокобезопасности веб-приложений

var httpClient = new HttpClient() {BaseAddress = new Uri("https://localhost:7166")};
await httpClient.GetAsync("/products/clear"); //Очищаем список товаров
await httpClient.GetAsync("/");
int i = 0;
await Parallel.ForEachAsync(Product.GenerateProducts(100), async (product, _) =>
{
    //await httpClient.PostAsJsonAsync("/catalog/add_with_lock", product);
    var str = await httpClient.GetStringAsync("/headers");
    //Console.WriteLine($"{product.Name} added");
    Console.WriteLine(str);
    Console.WriteLine($"\n\n\n{++i}------\n\n\n");
});

var result = await httpClient.GetFromJsonAsync<Product[]>("/products"); //"null"
Console.WriteLine("-----------------------------------");
Console.WriteLine(result!.Length);


record Product(string Name, double Price)
{
    public static IEnumerable<Product> GenerateProducts(ulong? count = null)
    {
        for (ulong i = 0; (i < count) || count is null; i++)
        {
            yield return new Product($"Product {i}", 1000);
        }
    }
}

