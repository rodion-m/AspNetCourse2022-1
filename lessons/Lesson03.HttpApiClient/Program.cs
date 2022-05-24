// See https://aka.ms/new-console-template for more information

using Lesson03.HttpApiClient;

var shopClient = new ShopClient("http://localhost:5198/");

var products = await shopClient.GetProducts();

Console.WriteLine(products);