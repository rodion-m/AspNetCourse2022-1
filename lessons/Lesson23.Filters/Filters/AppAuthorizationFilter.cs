using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class AppAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        //_logger.LogInformation(context.Result.ToString());
        //if (context.Result is UnauthorizedResult)
        //{
        context.Result = new UnauthorizedObjectResult(new ErrorModel("Not authorized"));
        //}
    }
}