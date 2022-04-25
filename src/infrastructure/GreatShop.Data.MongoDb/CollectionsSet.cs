using GreatShop.Domain.Entities;
using MongoDB.Driver;

namespace GreatShop.Data.MongoDb;

internal record CollectionsSet(IMongoCollection<Account> Accounts, IMongoCollection<Cart> Carts);