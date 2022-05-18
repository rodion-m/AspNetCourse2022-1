using Lesson08.Configurations.Services;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddSingleton<IEmailSender>(
//     new SmtpEmailSender("smtp.beget.com", "asp2022@rodion-m.ru", "aHGnOlz7")
// );

builder.Services.Configure<SmtpCredentials>(
    builder.Configuration.GetSection("SmtpCredentials"));
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
// builder.Services.AddScoped<UserData>(provider =>
// {
//     var stateManager = provider.GetService<StateManager>();
// });

//string host = builder.Configuration["SmtpCredentials:Host"];
// var credentials = builder.Configuration
//     .GetSection("SmtpCredentials")
//     .Get<SmtpCredentials>();
// builder.Services.AddSingleton<IEmailSender>(new SmtpEmailSender(credentials));
// var host = credentials.Host;
//throw new Exception(host);

// IConfigurationRoot config =
//     new ConfigurationBuilder()
//         .SetBasePath(Directory.GetCurrentDirectory())
//         .AddJsonFile("appsettings.json", true)
//         .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ENVIRONMENT")}.json", true)
//         .AddUserSecrets<Program>(true)
//         .AddEnvironmentVariables()
//         .AddCommandLine(args)
//         .Build();

// builder.Services.AddScoped<IEmailSender>(provider => new SmtpEmailSender("params..."));
// builder.Services.AddTransient<IEmailSender>(IServiceProvider => new SmtpEmailSender("params..."));

var app = builder.Build();

app.MapGet("/", (IEmailSender emailSender)
    => emailSender.SendAsync("asp2022@rodion-m.ru", "rody66@yandex.ru", "asd", "dsa"));

app.Run();