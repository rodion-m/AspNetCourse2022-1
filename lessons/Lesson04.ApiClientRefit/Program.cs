// See https://aka.ms/new-console-template for more information

using Lesson04.ApiClientRefit;
using Refit;

var shopClient = RestService.For<IShopClient>("http://localhost:5198/");
var allProducts = await shopClient.GetAllProducts();

Console.WriteLine(allProducts.Count);