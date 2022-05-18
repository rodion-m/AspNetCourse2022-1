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

var syncRoot = new object();
app.MapPost("/products/add_with_lock", (Product product) =>
{
    lock (syncRoot)
    {
        products.Add(product);
    }
});

app.MapGet("/headers_easy", (HttpContext context) => context.Request.Headers);

#endregion

app.Run();

record Product(string Name, double Price);