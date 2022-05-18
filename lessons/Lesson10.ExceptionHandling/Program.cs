using Lesson10.ExceptionHandling;
using Lesson08.Configurations.Services;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");
try
{
    /*
     * тут должна находиться вся логика создания веб сервера
     * (содержимое файла Program.cs: builder, app и тд)
     */
    
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.Configure<SmtpCredentials>(
        builder.Configuration.GetSection("SmtpCredentials"));
    builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
    builder.Services.AddHostedService<MailingBackgroundService>();
    builder.Host.UseSerilog((ctx, conf) =>
    {
        conf
            .MinimumLevel.Debug()
            .WriteTo.File("log-.log", rollingInterval: RollingInterval.Day)
            .WriteTo.Console(LogEventLevel.Information)
            .ReadFrom.Configuration(ctx.Configuration)
            .Enrich.FromLogContext()
            ;
    });
    builder.WebHost.UseSentry();

    var app = builder.Build();
    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
    
    app.MapGet("/", async (ILogger<Program> logger) =>
    {
        await Task.Delay(1000);
        logger.LogDebug("This is debug message");
        return "Hello World!";
    });

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