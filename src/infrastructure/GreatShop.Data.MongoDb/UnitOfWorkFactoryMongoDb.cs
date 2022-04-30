using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb;

public class UnitOfWorkFactoryMongoDb : IUnitOfWorkFactory
{
    private readonly IMongoClient _client;
    private readonly CollectionsSet _collections;
    private TransactionOptions? _transactionOptions;

    internal UnitOfWorkFactoryMongoDb(IMongoClient client, string dbName)
    {
        if (dbName == null) throw new ArgumentNullException(nameof(dbName));
        _client = client ?? throw new ArgumentNullException(nameof(client));
        UnitOfWorkMongoDb.RegisterMappings();
        var db = _client.GetDatabase(dbName);
        _collections = new CollectionsSet(
            db.GetCollection<Account>("accounts")!,
            db.GetCollection<Cart>("carts")!,
            db.GetCollection<Product>("products")!
        );
    }

    //TODO IOptions<MongodbConfig>
    public UnitOfWorkFactoryMongoDb(string connectionString, string dbName)
        : this(new MongoClient(connectionString), dbName)
    {
    }

    public async Task<IUnitOfWork> CreateAsync(
        bool startTransactionImmediately = true,
        TransactionIsolationLevel isolationLevel = TransactionIsolationLevel.Default,
        CancellationToken cancellationToken = default)
    {
        _transactionOptions = CreateTransactionOptions(isolationLevel);
        var sessionOptions = CreateSessionOptions(_transactionOptions);
        var session = await _client.StartSessionAsync(sessionOptions, cancellationToken);
        var unitOfWorkMongoDb = new UnitOfWorkMongoDb(_collections, session);
        if (startTransactionImmediately)
        {
            unitOfWorkMongoDb.StartTransaction(_transactionOptions);
        }

        return unitOfWorkMongoDb;
    }

    private ClientSessionOptions CreateSessionOptions(TransactionOptions defaultTransactionOptions)
    {
        return new ClientSessionOptions()
        {
            DefaultTransactionOptions = defaultTransactionOptions
        };
    }

    private TransactionOptions CreateTransactionOptions(TransactionIsolationLevel isolationLevel)
    {
        return new TransactionOptions(); //TODO
    }
}