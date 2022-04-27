namespace GreatShop.Domain.Repositories;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IAccountRepository AccountRepository { get; }
    ICartRepository CartRepository { get; }
    IProductRepository ProductRepository { get; }
    bool IsCommited { get; }
    
    ValueTask CommitAsync(CancellationToken cancellationToken = default);
}