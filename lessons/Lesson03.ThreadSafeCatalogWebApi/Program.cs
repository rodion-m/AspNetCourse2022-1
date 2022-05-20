using System.Text.Json;

var products = new List<Product>();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");
#region Rodion

app.MapGet("/products", () => products);

app.MapGet("/products/clear", () => products.Clear());

app.MapPost("/products/add", (Product product) => products.Add(product));

app.MapPost("/products/add_with_lock", (Product product) =>
{
    lock (products) //<-- Установка барьера
    {
        products.Add(product);
    }
});

app.MapPost("/products/add_with_parametrs", (string name, int price) =>
{
    lock (products) //<-- Установка барьера
    {
        products.Add(new Product(name, price));
    }
});

app.MapPost("/products/add_list", (Product[] product) =>
{
    lock (products)
    {
        products.AddRange(product);
    }
});

var categories = new List<int>();
app.MapPost("/cats/add", (int cat) =>
{
    lock (categories)
    {
        categories.Add(cat);
    }

});

 async void Button1_Click() 
 {
    //Thread.Sleep(3000);
    //Task.Delay(3000).WaitAsync(CancellationToken.None); //Thread.Sleep(3000);
    await Task.Delay(3000);
    var text = await File.ReadAllTextAsync("file.txt");
    await Task.Delay(3000);
    Environment.Exit(0);
}


app.MapGet("/headers_easy", (HttpContext context) => context.Request.Headers);

#endregion

app.MapGet("/save_products", () =>
{
    string json = JsonSerializer.Serialize(products);
    File.WriteAllText("products.txt", json);
    //10_000 потоков потреб. ресурсы системы
});

app.MapGet("/save_products_async", async () =>
{
    string json = JsonSerializer.Serialize(products);
    //1000 потоков
    await File.WriteAllTextAsync("products.txt", json);
    //0 потоков
});


app.Run();

record Product(string Name, double Price);