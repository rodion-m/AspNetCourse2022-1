using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb.Repositories;

internal class AccountRepository : MongoGenericRepository<Account>, IAccountRepository
{
    public AccountRepository(IMongoCollection<Account> collection, IClientSessionHandle session) 
        : base(collection, session)
    {
    }

    public Task<Account?> FindByEmail(string email, CancellationToken cancellationToken)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        return Collection.Find(Session, it => it.Email == email)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)!;
    }

    public Task<bool> IsAccountExist(string email, CancellationToken cancellationToken)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        return Collection.Find(Session, it => it.Email == email)
            .AnyAsync(cancellationToken: cancellationToken)!;
    }
}