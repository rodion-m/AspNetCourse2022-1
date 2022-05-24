using ServiceProviderChecks.Deps;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();

builder.Services.AddSingleton<Singleton1>();
builder.Services.AddSingleton<Singleton2>();
builder.Services.AddScoped<Scoped1>();
builder.Services.AddScoped<Scoped2>();
builder.Services.AddScoped<Scoped3>();
builder.Services.AddTransient<Transient1>();
builder.Services.AddTransient<Transient2>();
builder.Services.AddTransient<Transient3>();

var provider = builder.Services.BuildServiceProvider();
/*
 * In two cases below
 * IServiceProvider and IServiceScopeFactory instances are identical
 * because there is no any scope.
 */
provider!.GetService<Singleton1>();
provider!.GetService<Transient3>();

var app = builder.Build();
app.MapControllers();

app.MapGet("/", 
    (
        Singleton1 singleton1, 
        Singleton2 singleton2, 
        Scoped1 scoped1,
        Transient1 transient1,
        Transient2 transient2,
        IServiceProvider serviceProvider, 
        IServiceScopeFactory serviceScopeFactory, 
        ILogger<Program> logger
        ) =>
{
    logger.LogWarning("ServiceProvider: {ServiceProvider}", serviceProvider.GetHashCode());
    logger.LogWarning("ServiceScopeFactory: {ServiceScopeFactory}", serviceScopeFactory.GetHashCode());
    serviceProvider.GetRequiredService<Scoped2>();
    serviceProvider.CreateScope()
        .ServiceProvider.GetRequiredService<Scoped3>();
});

app.Run();
