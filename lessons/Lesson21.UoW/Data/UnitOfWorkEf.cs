namespace Lesson21.UoW.Data;

public class UnitOfWorkEf : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWorkEf(
        AppDbContext dbContext,
        IAccountRepository accountRepository,
        ICartRepository cartRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        CartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    }

    public IAccountRepository AccountRepository { get; }
    public ICartRepository CartRepository { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }
}