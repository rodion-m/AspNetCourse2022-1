﻿// See https://aka.ms/new-console-template for more information

using Lesson03_HttpClient;

var shopClient = new ShopClient("http://localhost:5198/");

Console.WriteLine(await shopClient.GetProducts());