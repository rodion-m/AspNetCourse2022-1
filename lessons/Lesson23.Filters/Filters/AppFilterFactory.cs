using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

public class UsefulAuthFilterAttribute : Attribute, IFilterFactory
{
    public bool IsReusable => false;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetService<ILogger<AppAlwaysRunResultFilter>>();
        var repository = serviceProvider.GetService<IAccountRepository>();
        return new AppAlwaysRunResultFilter(logger, repository);
    }
}