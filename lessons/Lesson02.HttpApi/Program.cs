using Lesson04.HttpModels;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/customs_duty", Index);

string Index(ICatalog catalog)
{
    if (catalog == null) 
        throw new ArgumentNullException(nameof(catalog));

    return catalog.GetProducts(); //NullReferenceException (NRE/NPE)
}

// int CalculateDaysBeforeNewYear()
// {
//     //now_days 1000, next_year_days 1200
//     //1000 + 200 = 1200
//     //daysRemainNewYear = next_year_days - now_days;
//     var now = DateTime.Now;
//     var nextYear = new DateTime(now.Year + 1, 1, 1);
//     TimeSpan daysRemainNewYear = nextYear - now;
//     int result = daysRemainNewYear.Days;
//     return result;
// }
//
// app.MapGet("/new_year", CalculateDaysBeforeNewYear);
//
// app.Run();










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