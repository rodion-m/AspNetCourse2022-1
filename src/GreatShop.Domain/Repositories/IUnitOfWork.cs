namespace GreatShop.Domain.Repositories;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    ICartRepository CartRepository { get; }
    IAccountRepository AccountRepository { get; }
    bool IsCommited { get; }
    
    ValueTask CommitAsync(CancellationToken cancellationToken = default);
}