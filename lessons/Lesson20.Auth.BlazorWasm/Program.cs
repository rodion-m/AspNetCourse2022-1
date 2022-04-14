using Blazored.LocalStorage;
using Lesson14.HttpClient;
using Lesson20.Auth.BlazorWasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(new ShopClient("https://localhost:7207"));
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();