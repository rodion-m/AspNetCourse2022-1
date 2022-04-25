using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly MongoClient _client;
    private readonly CollectionsSet _collections;

    public UnitOfWorkFactory(string connectionString, string dbName)
    {
        if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
        if (dbName == null) throw new ArgumentNullException(nameof(dbName));
        _client = new MongoClient(connectionString);
        var db = _client.GetDatabase(dbName);
        _collections = new CollectionsSet(
            db.GetCollection<Account>("accounts")!,
            db.GetCollection<Cart>("carts")!
        );
    }

    public async Task<IUnitOfWork> CreateAsync(
        bool startTransactionImmediately = true, 
        CancellationToken cancellationToken = default)
    {
        var session = await _client.StartSessionAsync(new ClientSessionOptions(), cancellationToken);
        var unitOfWorkMongoDb = new UnitOfWorkMongoDb(_collections, session);
        if (startTransactionImmediately)
        {
            unitOfWorkMongoDb.StartTransaction();
        }
        return unitOfWorkMongoDb;
    }
}