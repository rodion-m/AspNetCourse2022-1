using Lesson7_Mail.BackgroundServices;
using Lesson7_Mail.Services;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
builder.Services.AddHostedService<MailingBackgroundService>();
builder.Services.AddHostedService<ExampleBackgroundService>();

builder.Services.Configure<HostOptions>(
    opts =>
    {
        opts.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
    });


builder.Services.Configure<HostOptions>(
    opts => opts.ShutdownTimeout = TimeSpan.FromSeconds(10));
builder.Services.PostConfigureAll<HostOptions>(opts =>
    opts.ShutdownTimeout = TimeSpan.FromSeconds(20));
builder.WebHost.UseShutdownTimeout(TimeSpan.FromSeconds(30));

builder.WebHost.UseSerilog((context, configuration) => configuration.WriteTo.Console());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/stop", (
    IHostApplicationLifetime lifetime,
    CancellationToken cancellationToken,
    IOptions<HostOptions> options,
    ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("stop");
    logger.LogInformation(logger.GetType().ToString());
    return Results.Ok(options.Value.ShutdownTimeout);
    while (true)
        //cancellationToken.ThrowIfCancellationRequested();
        lifetime.ApplicationStopping.ThrowIfCancellationRequested();
});

app.Run();