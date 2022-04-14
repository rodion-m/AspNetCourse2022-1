using Lesson13.EFCore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

app.MapGet("/orders", async (AppDbContext context)
    =>
{
    var listAsync = await context.Orders.ToListAsync();
    return listAsync;
});

app.Run();