using Lesson14.HttpApi.Data;
using Lesson14.HttpApi.Services;
using Lesson14.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

app.MapGet("/orders/get_all", (IOrderRepository orderRepository)
    => orderRepository.GetAll());

app.MapPost("/orders/offer", (OrdersService service, Order order)
    => service.OfferOrder(order));


app.Run();