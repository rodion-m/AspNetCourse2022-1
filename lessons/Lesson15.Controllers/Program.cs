using Lesson15.Controllers.Controllers;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "myapp.db";
// builder.Services.AddDbContext<AppDbContext>(
//     options => options.UseSqlite($"Data Source={dbPath}"));
// builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

builder.Services.AddControllers();
builder.Services.AddSingleton<CatalogService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.MapControllers();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}"
);

app.Run();