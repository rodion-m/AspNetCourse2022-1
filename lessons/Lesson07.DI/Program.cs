using Lesson7.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<SomeService>();
builder.Services.AddTransient<LifeTimeTester>();
//builder.Services.AddScoped<LifeTimeTester>();

builder.Services.AddSingleton<SingletonService>();
builder.Services.AddSingleton<IClock, RealClock>();
// builder.Services.AddSingleton<IClock>(
//     new FakeClock(new DateTime(2013, 1, 1))
// );

var app = builder.Build();

app.MapDefaultControllerRoute();
//См. контроллер
app.Map("/single", (SingletonService singletonService) =>
{
    return singletonService.ToString();
});

app.Run();