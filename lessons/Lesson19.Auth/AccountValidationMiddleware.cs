using System.Security.Claims;
using Lesson19.Auth.Data;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Lesson19.Auth;

public class AccountValidationMiddleware
{
    private readonly RequestDelegate _next;

    public AccountValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AccountRepository repository)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var subject = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(subject);
            var iat = context.User.FindFirstValue(JwtRegisteredClaimNames.Iat);
            var tokenCreatedAt = DateTimeOffset.FromUnixTimeSeconds(long.Parse(iat));
            var account = repository.GetById(userId);
            if (tokenCreatedAt < account.AllTokensBlockedAt)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Token is blocked." });
                return;
            }
        }

        await _next(context);
    }
}