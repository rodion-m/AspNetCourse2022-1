using Lesson17.PasswordHashing;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<PasswordHasherOptions>(
    opt =>
    {
        opt.IterationCount = 100_000;
        opt.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
    });
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
var app = builder.Build();

var _hashedPassword = "";
app.MapGet("/", (IPasswordHasher<User> hasher) =>
{
    var password = "1234567890";
    var user = new User();
    var hashedPassword = hasher.HashPassword(user, password);
    _hashedPassword = hashedPassword;
    return hashedPassword;
});

app.MapGet("/hash",
    (string pwd, IPasswordHasher<User> hasher) =>
    {
        var hashedPassword = hasher.HashPassword(new User(), pwd);
        return hashedPassword;
    });

app.MapGet("/check",
    (string pwd, IPasswordHasher<User> hasher) =>
    {
        var result = hasher.VerifyHashedPassword(
            new User(), _hashedPassword, pwd);
        return result.ToString();
    });

app.Run();