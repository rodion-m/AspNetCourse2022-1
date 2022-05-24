using Lesson04.HttpModels;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello world");

var products = new List<Product>(
    new[]
    {
        new Product(Guid.NewGuid(), "Чистый код", 1000m),
        new Product(Guid.NewGuid(), "Элегантные объекты", 1200m),
        new Product(Guid.NewGuid(), "Чистая архитектура", 1500m)
    });

app.MapGet("/products/all", () => products);
app.MapPost("/products/add", (Product p) => products.Add(p));

// REST Delete Example 
app.MapDelete("/v1/users/{userId}/albums/{albumId}",
    ([FromRoute] string userId, [FromRoute] string albumId)
        =>
    {
        DeleteAlbum(userId, albumId);
    });

void DeleteAlbum(string userId, string albumId)
{
    Console.WriteLine(userId);
    //...
}

app.Run();