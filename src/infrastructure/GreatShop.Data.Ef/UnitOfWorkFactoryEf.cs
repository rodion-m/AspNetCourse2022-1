using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef;

public class UnitOfWorkFactoryEf : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public UnitOfWorkFactoryEf(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
    }
    
    public async Task<IUnitOfWork> CreateAsync(
        bool startTransactionImmediately = true,
        TransactionIsolationLevel isolationLevel = TransactionIsolationLevel.Default,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await _contextFactory.CreateDbContextAsync(cancellationToken);
        if (startTransactionImmediately)
        {
            //await dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        return new UnitOfWorkEf(dbContext);
    }
}