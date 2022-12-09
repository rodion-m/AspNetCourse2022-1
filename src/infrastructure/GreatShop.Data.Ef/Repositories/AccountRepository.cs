using GreatShop.Domain.Entities;
using GreatShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Data.Ef.Repositories;

internal class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Account?> FindByEmail(string email, CancellationToken cancellationToken)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        return Entities.FirstOrDefaultAsync(it => it.Email == email, cancellationToken: cancellationToken);
    }
}