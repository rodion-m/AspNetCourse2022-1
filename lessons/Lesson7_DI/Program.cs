using Lesson7_DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<SomeService>();
builder.Services.AddTransient<LifeTimeTester>();

builder.Services.AddSingleton<SingletonService>();

var app = builder.Build();

app.MapDefaultControllerRoute();
app.Map("/single", (SingletonService singletonService) => singletonService.ToString());

await Task.Delay(10000).WaitAsync(new CancellationTokenSource(1000).Token);

app.Run();