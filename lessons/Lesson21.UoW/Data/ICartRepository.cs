using Lesson14.Models;

namespace Lesson21.UoW.Data;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetByAccountId(Guid accountId);
}