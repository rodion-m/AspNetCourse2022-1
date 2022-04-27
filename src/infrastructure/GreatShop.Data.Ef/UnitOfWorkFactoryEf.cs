using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef;

public class UnitOfWorkFactoryEf : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public UnitOfWorkFactoryEf(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task<IUnitOfWork> CreateAsync(
        bool startTransactionImmediately = true, 
        CancellationToken cancellationToken = default)
    {
        var dbContext = await _contextFactory.CreateDbContextAsync(cancellationToken);
        return new UnitOfWorkEf(dbContext);
    }
}