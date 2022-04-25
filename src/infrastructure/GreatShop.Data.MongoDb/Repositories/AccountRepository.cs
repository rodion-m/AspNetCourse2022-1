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
}