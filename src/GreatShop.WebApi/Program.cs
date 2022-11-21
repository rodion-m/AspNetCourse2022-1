using GreatShop.Configurations;
using GreatShop.Data.Ef;
using GreatShop.Domain;
using GreatShop.Domain.Repositories;
using GreatShop.Domain.Services;
using GreatShop.Infrastructure;
using GreatShop.WebApi;
using GreatShop.WebApi.Extensions;
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

    builder.Services.Configure<DbConfig>(builder.Configuration.GetSection("DbConfig"));
    //dotnet ef migrations add Init -p ../MyShop.Data.Ef/
    builder.Services.AddDbContextFactory<AppDbContext>();
    builder.Services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactoryEf>();
    //builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetService<IUnitOfWorkFactory>().CreateAsync());
    //builder.Services.AddRepository<IProductRepository>(uow => uow.ProductRepository);
    
    builder.Services.AddSingleton<IClock, UtcClock>();
    builder.Services.AddSingleton<HttpModelsMapper>();

    builder.Services.AddScoped<CatalogService>();

    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration);
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.UseAuthorization();

    app.MapControllers();
    app.MapGet("/ping", () => "pong");

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception on server startup");
    throw;
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush(); // Перед выходом дожидаемся пока все логи будут записаны (сохранены)
}