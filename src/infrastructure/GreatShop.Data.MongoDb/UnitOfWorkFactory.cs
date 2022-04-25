using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IMongoClient _client;
    private readonly CollectionsSet _collections;

    public UnitOfWorkFactory(IMongoClient client, string dbName)
    {
        if (dbName == null) throw new ArgumentNullException(nameof(dbName));
        _client = client ?? throw new ArgumentNullException(nameof(client));
        var db = _client.GetDatabase(dbName);
        _collections = new CollectionsSet(
            db.GetCollection<Account>("accounts")!,
            db.GetCollection<Cart>("carts")!
        );
    }
    public UnitOfWorkFactory(string connectionString, string dbName)
        : this(new MongoClient(connectionString), dbName)
    {
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