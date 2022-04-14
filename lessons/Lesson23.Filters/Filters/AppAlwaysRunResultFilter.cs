using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lesson23.Filters.Filters;

class AppAlwaysRunResultFilter : IAlwaysRunResultFilter
{
    private readonly ILogger<AppAlwaysRunResultFilter> _logger;
    private readonly IAccountRepository _accountRepository;

    public AppAlwaysRunResultFilter(ILogger<AppAlwaysRunResultFilter> logger, IAccountRepository accountRepository)
    {
        _logger = logger;
        _accountRepository = accountRepository;
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        _accountRepository.GetById(Guid.Empty).GetAwaiter().GetResult();
        _logger.LogInformation("OnResultExecuted: {Result}", context.Result);
    }
}