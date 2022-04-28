namespace GreatShop.Domain.Repositories;

public interface IUnitOfWorkFactory
{
    Task<IUnitOfWork> CreateAsync(
        bool startTransactionImmediately = true,
        TransactionIsolationLevel isolationLevel = TransactionIsolationLevel.Default,
        CancellationToken cancellationToken = default
    );
}

public enum TransactionIsolationLevel
{
    Default
}