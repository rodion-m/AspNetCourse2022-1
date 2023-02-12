using GreatShop.Data.Ef.Repositories;
using GreatShop.Domain.Repositories;

namespace GreatShop.Data.Ef;

/// <summary>
/// Unit of work implementation for Entity Framework.
/// This class is not thread-safe.
/// </summary>
internal class UnitOfWorkEf : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWorkEf(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    private IAccountRepository? _accountRepository;
    public IAccountRepository AccountRepository
    {
        get
        {
            return _accountRepository ??= new AccountRepository(_dbContext);
        }
    }
    
    private ICartRepository? _cartRepository;
    public ICartRepository CartRepository
    {
        get { return _cartRepository ??= new CartRepository(_dbContext); }
    }
    
    private IProductRepository? _productRepository;
    public IProductRepository ProductRepository
    {
        get { return _productRepository ??= new ProductRepository(_dbContext); }
    }
    
    public bool IsCommitted => !_dbContext.ChangeTracker.HasChanges();
    
    public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose() => _dbContext.Dispose();

    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
}