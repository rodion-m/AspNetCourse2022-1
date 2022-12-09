using GreatShop.Domain.Entities;

namespace GreatShop.Domain.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> FindByEmail(string email, CancellationToken cancellationToken = default);
}