using System;
using System.Net.Http;
using System.Threading.Tasks;
using GreatShop.Data.Ef;
using GreatShop.Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace GreatShop.WebApi.Test;


public class IntegrationTests : IDisposable
{
    private readonly WebApplicationFactory<Program> _application;

    public IntegrationTests(ITestOutputHelper output)
    {
        _application = CreateTestServer(output, "test.db");
    }

    private WebApplicationFactory<Program> CreateTestServer(ITestOutputHelper output, string dbPath)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                //Тут можно сделать доп. настройки сервера.
                builder.UseEnvironment("Testing");
                builder.ConfigureTestServices(services =>
                {
                    //Заменяем модуль отправки писем на заглушку
                    services.RemoveAll<IEmailSender>();
                    services.AddSingleton<IEmailSender, StubEmailSender>();
                    
                    services.RemoveAll<AppDbContext>();
                    services.AddDbContext<AppDbContext>(
                        options => options.UseSqlite($"Data Source={dbPath}"));

                    // Создаем таблицы:
                    using var serviceProvider = services.BuildServiceProvider();
                    using var context = serviceProvider.GetService<AppDbContext>();
                    context!.Database.EnsureCreated();
                });
                
                //Заменяем приемник для логов на тестовый вывод
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

    public void Dispose()
    {
        //_application.Server.Services.GetService()
        using (var serviceScope = _application.Server.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            // Удаляем тестовую БД:
            context!.Database.EnsureDeleted();
        }
        _application.Dispose(); //Разрушаем веб-приложение
    }
}

public class StubEmailSender : IEmailSender
{
}