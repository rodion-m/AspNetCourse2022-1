using GreatShop.Data.MongoDb.Repositories;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb;

// NOTE: SESSIONS ARE NOT THREAD SAFE
internal class UnitOfWorkMongoDb : IUnitOfWork
{
    private IAccountRepository? _accountRepository;
    public IAccountRepository AccountRepository
    {
        get { return _accountRepository ??= new AccountRepository(_collections.Accounts, _session); }
    }
    
    private ICartRepository? _cartRepository;
    public ICartRepository CartRepository
    {
        get { return _cartRepository ??= new CartRepository(_collections.Carts, _session); }
    }
    private IProductRepository? _productRepository;
    public IProductRepository ProductRepository
    {
        get { return _productRepository ??= new ProductRepository(_collections.Products, _session); }
    }

    public bool IsCommited { get; private set; }

    private readonly CollectionsSet _collections;
    private readonly IClientSessionHandle _session;

    public UnitOfWorkMongoDb(
        CollectionsSet collections, 
        IClientSessionHandle session)
    {
        _collections = collections ?? throw new ArgumentNullException(nameof(collections));
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }

    public void StartTransaction(TransactionOptions? transactionOptions = null)
    {
        _session.StartTransaction(transactionOptions);
    }

    public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
    {
        await _session.CommitTransactionAsync(cancellationToken);
        IsCommited = true;
    }
    
    public void Dispose()
    {
        if (_session.IsInTransaction)
        {
            _session.AbortTransaction();
        }
        _session.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_session.IsInTransaction)
        {
            await _session.AbortTransactionAsync();
        }
        _session.Dispose();
    }

}