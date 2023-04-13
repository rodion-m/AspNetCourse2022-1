using System.Threading.Tasks;
using GreatShop.Data.Ef;
using GreatShop.Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;
using Xunit.Abstractions;
using AppDbContext = GreatShop.Data.Ef.AppDbContext;

namespace GreatShop.WebApi.IntegrationTest;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private const string DbPath = "test.db";

    public CustomWebApplicationFactory()
    {
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
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
                options => options.UseSqlite($"Data Source={DbPath}")
            );
            
            CreateDbTables(services);
        });
                
        //Заменяем приемник для логов на тестовый вывод
        // ITestOutputHelperAccessor
        //NuGet пакет: Serilog.Sinks.XUnit from xUnit.DependencyInjection
        //https://github.com/xunit/xunit/commit/89d11eca58bed6754304a2b3897bf2a0f6dcd838
         // builder.UseSerilog((_, config) =>
         //     config.WriteTo.TestOutput(_diagnosticMessageSink)
         // );
    }

    private static void CreateDbTables(IServiceCollection services)
    {
        // Создаем таблицы:
        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        using AppDbContext dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();
    }

    public override async ValueTask DisposeAsync()
    {
        using (var serviceScope = Server.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Удаляем тестовую БД:
            await context!.Database.EnsureDeletedAsync();
        }
        await base.DisposeAsync();
    }
}