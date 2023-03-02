using System.Threading.Tasks;
using GreatShop.Data.Ef;
using GreatShop.Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GreatShop.WebApi.Test;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    private const string DbPath = "test.db";

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
                options => options.UseSqlite($"Data Source={DbPath}"));

            // Создаем таблицы:
            using var serviceProvider = services.BuildServiceProvider();
            using var context = serviceProvider.GetService<AppDbContext>();
            context!.Database.EnsureCreated();
        });
                
        //Заменяем приемник для логов на тестовый вывод
        // ITestOutputHelperAccessor
        //NuGet пакет: Serilog.Sinks.XUnit from xUnit.DependencyInjection
        //https://github.com/xunit/xunit/commit/89d11eca58bed6754304a2b3897bf2a0f6dcd838
        // builder.UseSerilog((_, config) =>
        //     config.WriteTo.TestOutput(_output)
        // );
    }

    public override async ValueTask DisposeAsync()
    {
        using (var serviceScope = Server.Services.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            // Удаляем тестовую БД:
            await context!.Database.EnsureDeletedAsync();
        }
        await base.DisposeAsync();
    }
}