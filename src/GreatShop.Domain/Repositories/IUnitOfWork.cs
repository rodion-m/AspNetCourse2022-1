namespace GreatShop.Domain.Repositories;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IAccountRepository AccountRepository { get; }
    ICartRepository CartRepository { get; }
    IProductRepository ProductRepository { get; }
    bool IsCommitted { get; }
    
    ValueTask CommitAsync(CancellationToken cancellationToken = default);
}