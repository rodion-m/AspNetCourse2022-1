using Lesson23.Filters;
using Lesson23.Filters.Filters;
using Lesson23.Filters.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CentralizedExceptionHandlingFilter>(order: 0); //добавляем фильтр в пайплайн MVC
    options.Filters.Add<AppExceptionFilter2>(order: 1);
    options.Filters.Add<LogResourceFilter>();
    options.Filters.Add<ParametersLoggingActionFilter>();
    //options.Filters.Add<AppAlwaysRunResultFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(op =>
{
    op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();

builder.Services.AddSingleton<ResponseDefaultFormatterService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AppAuthorizationFilter>();

builder.Host.UseSerilog((_, conf) =>
{
    conf.MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("log-.log", rollingInterval: RollingInterval.Day)
        .Enrich.FromLogContext();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<UnauthorizedResponseModelMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", [CentralizedExceptionHandlingFilter] (_) => throw new Exception("Hello Exception"));

app.MapControllers();

    
app.Run(); //app.UseMiddleware<EndpointMiddleware>();