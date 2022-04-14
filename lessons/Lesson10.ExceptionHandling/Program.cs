using Lesson10.ExceptionHandling;
using Lesson8.Configurations.Services;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SmtpCredentials>(
    builder.Configuration.GetSection("SmtpCredentials"));
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
builder.Services.AddHostedService<MailingBackgroundService>();
//builder.Host.UseSerilog((_, conf) => conf.WriteTo.Console());
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
    await Task.Delay(2000);
    logger.LogDebug("This is debug");
    return "Hello World!";
});

app.Run();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");
try
{
    /*
     * тут должна находится вся логика создания веб сервера
     * (содержимое файла Program.cs: builder, app и тд)
     */
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush(); // Перед выходом дожидаемся пока все логи будут записаны
}