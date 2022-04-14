using System.Net.Http;
using System.Threading.Tasks;
using GreatShop.Domain.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace GreatShop.WebApi.Test;

public class IntegrationTests
{
    private readonly WebApplicationFactory<Program> _application;

    public IntegrationTests(ITestOutputHelper output)
    {
        _application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                //Тут можно сделать доп настройки сервера.
                //Например, специальное логирование.
                
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<IEmailSender>();
                    services.AddSingleton<IEmailSender, StubEmailSender>();
                });
                
                //NuGet пакет: Serilog.Sinks.XUnit
                builder.UseSerilog((_, config) =>
                    config.WriteTo.TestOutput(output)
                );
            });
    }

    [Fact]
    public async Task Ping_endpoint_returns_pong()
    {
        HttpClient client = _application.CreateClient();
        // Здесь может быть создание ShopClient
        var response = await client.GetAsync("/ping");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("pong", content);
    }
}

public class StubEmailSender : IEmailSender
{
}