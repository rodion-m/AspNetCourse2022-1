using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;

namespace GreatShop.Data.Ef.Repositories;

internal class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}