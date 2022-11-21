// See https://aka.ms/new-console-template for more information

using Lesson04.ApiClientRefit;
using Microsoft.Extensions.DependencyInjection;
using Refit;

IServiceCollection services = null!;
services.AddSingleton(
    RestService.For<IShopClient>("http://localhost:5198")
);

services.AddRefitClient<IShopClient>()
    .ConfigureHttpClient(
        httpClient => httpClient.BaseAddress = new Uri("http://localhost:5198")
    );


IShopClient shopClient = RestService.For<IShopClient>("http://localhost:5198/");
var allProducts = await shopClient.GetAllProducts();

Console.WriteLine(allProducts.Count);