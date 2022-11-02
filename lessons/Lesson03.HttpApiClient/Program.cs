using Lesson03.HttpApiClient;
using Lesson04.HttpModels;

var shopClient = new ShopClient("http://localhost:5198/");

var products = await shopClient.GetProducts();

Console.WriteLine(products);