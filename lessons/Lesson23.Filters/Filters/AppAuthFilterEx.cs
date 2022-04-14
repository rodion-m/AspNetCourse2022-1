using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class AppAuthFilterAttribute : Attribute, IAuthorizationFilter, IAppOrderedFilter
{
    public FilterOrder Order { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
    }
}

public interface IAppOrderedFilter : IOrderedFilter
{
    new FilterOrder Order { get; set; }
    int IOrderedFilter.Order => (int) Order;
}