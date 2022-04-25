using GreatShop.Domain.Repositories;

namespace GreatShop.Data.MongoDb;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    ICartRepository CartRepository { get; }
    IAccountRepository AccountRepository { get; }
    
    ValueTask CommitAsync(CancellationToken cancellationToken = default);
}