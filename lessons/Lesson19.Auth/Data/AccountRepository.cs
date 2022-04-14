using Lesson14.Models;

namespace Lesson19.Auth.Data;

public class AccountRepository
{
    public async Task<Account?> FindByEmail(string email)
    {
        return new Account
        {
            Id = new Guid(),
            Email = email
        };
    }

    public Account GetById(Guid id)
    {
        return new Account
        {
            Id = id,
            Email = "r@r.ru",
            AllTokensBlockedAt = DateTimeOffset.Now.AddMinutes(-10)
        };
    }
}