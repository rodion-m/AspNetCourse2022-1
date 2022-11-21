using Lesson03.HttpApiClient;
using Lesson04.HttpModels;

var backendHost = "http://localhost:5198/";
var shopClient = new ShopClient(backendHost);

Product product = await shopClient.GetProduct(100);

Console.WriteLine(product);