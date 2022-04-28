using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb;

public class UnitOfWorkFactoryMongoDb : IUnitOfWorkFactory
{
    private readonly IMongoClient _client;
    private readonly CollectionsSet _collections;

    public UnitOfWorkFactoryMongoDb(IMongoClient client, string dbName)
    {
        if (dbName == null) throw new ArgumentNullException(nameof(dbName));
        _client = client ?? throw new ArgumentNullException(nameof(client));
        var db = _client.GetDatabase(dbName);
        _collections = new CollectionsSet(
            db.GetCollection<Account>("accounts")!,
            db.GetCollection<Cart>("carts")!,
            db.GetCollection<Product>("products")!
        );
    }
    public UnitOfWorkFactoryMongoDb(string connectionString, string dbName)
        : this(new MongoClient(connectionString), dbName)
    {
    }

    public async Task<IUnitOfWork> CreateAsync(
        bool startTransactionImmediately = true,
        TransactionIsolationLevel isolationLevel = TransactionIsolationLevel.Default, //TODO
        CancellationToken cancellationToken = default)
    {
        var session = await _client.StartSessionAsync(null, cancellationToken);
        var unitOfWorkMongoDb = new UnitOfWorkMongoDb(_collections, session);
        if (startTransactionImmediately)
        {
            unitOfWorkMongoDb.StartTransaction(); //new TransactionOptions()
        }
        return unitOfWorkMongoDb;
    }
}