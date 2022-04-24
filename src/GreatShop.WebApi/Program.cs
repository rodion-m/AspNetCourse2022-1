using GreatShop.Data.Ef;
using GreatShop.Domain;
using GreatShop.Infrastructure;
using GreatShop.WebApi.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var dbPath = builder.Configuration["dbPath"];
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlite($"Data Source={dbPath}"));

    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration);
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSingleton<IClock, UtcClock>();
    builder.Services.AddSingleton<HttpModelsMapper>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapGet("/ping", () => "pong");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception on server startup");
    throw;
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush(); // Перед выходом дожидаемся пока все логи будут записаны (сохранены)
}