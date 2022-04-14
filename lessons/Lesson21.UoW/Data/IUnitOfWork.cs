namespace Lesson21.UoW.Data;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IAccountRepository AccountRepository { get; }
    ICartRepository CartRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}