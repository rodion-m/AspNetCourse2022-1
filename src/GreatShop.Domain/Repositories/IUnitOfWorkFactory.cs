using GreatShop.Data.MongoDb;

namespace GreatShop.Domain.Repositories;

public interface IUnitOfWorkFactory
{
    Task<IUnitOfWork> CreateAsync(
        bool startTransactionImmediately = true, 
        CancellationToken cancellationToken = default);
}