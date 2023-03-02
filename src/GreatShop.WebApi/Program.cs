using GreatShop.Configurations;
using GreatShop.Data.Ef;
using GreatShop.WebApi.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddOptions<DbConfig>()
        .BindConfiguration("DbConfig")
        .ValidateDataAnnotations()
        .ValidateOnStart();

    //dotnet ef migrations add Init -p ../MyShop.Data.Ef/
    builder.Services.AddDbContextFactory<AppDbContext>();
    
    builder.Services.AddDomainDependencies();

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

public partial class Program { }