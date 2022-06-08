﻿using System.Net.Http.Json;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
// Пушка для тестирования потокобезопасности веб-приложений

var httpClient = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7207")
};
//await httpClient.GetAsync("/products/clear"); //Очищаем список товаров
await httpClient.GetAsync("/");
await Parallel.ForEachAsync(Product.GenerateProducts(1000), async (product, _) =>
{
    await httpClient.PostAsync("/Catalogs/CreateGoods", product.ToFormData());
    Console.WriteLine($"added {product.Name}");
});
Console.WriteLine("Done");
return;


var result = await httpClient.GetFromJsonAsync<Product[]>("/products"); //"null"
Console.WriteLine("-----------------------------------");
Console.WriteLine(result!.Length);


record Product(string Name, long Article, decimal Price)
{
    public static IEnumerable<Product> GenerateProducts(long? count = null)
    {
        for (long i = 0; (i < count) || count is null; i++)
        {
            yield return new Product($"Product {i}", i, 1000);
        }
    }

    public FormUrlEncodedContent ToFormData()
    {
        return new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>(nameof(Name), Name), 
            new KeyValuePair<string, string>(nameof(Article), Article.ToString()),
            new KeyValuePair<string, string>(nameof(Price), Price.ToString()) 
        });
    }
}

