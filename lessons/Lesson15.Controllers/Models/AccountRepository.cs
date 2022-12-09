using Microsoft.EntityFrameworkCore;

namespace Lesson15.Controllers.Models;

public class AccountRepository : EfRepository<Account>
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Account> GetByEmail(string email)
    {
        return _entities.FirstAsync(it => it.Email == email);
    }
}