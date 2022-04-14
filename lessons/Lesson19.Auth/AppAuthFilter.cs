using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson19.Auth;

public class AppAuthFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        
    }
}