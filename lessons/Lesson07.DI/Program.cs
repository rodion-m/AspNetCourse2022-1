using Lesson7.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<SomeService>();
builder.Services.AddTransient<LifeTimeTester>();
//builder.Services.AddScoped<LifeTimeTester>();

builder.Services.AddSingleton<SingletonService>();

var app = builder.Build();

app.MapDefaultControllerRoute();
//См. контроллер
app.Map("/single", (SingletonService singletonService) =>
{
    return singletonService.ToString();
});

app.Run();