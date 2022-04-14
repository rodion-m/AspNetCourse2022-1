using Lesson14.Models;

namespace Lesson23.Filters;

public interface IAccountRepository
{
    Task<Order> GetById(Guid Id);
    Task<Order?> FindById(Guid Id);

    Task Add(Order order);
    Task Update(Order order);
}

class AccountRepository : IAccountRepository
{
    public Task<Order> GetById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> FindById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Order order)
    {
        throw new NotImplementedException();
    }

    public Task Update(Order order)
    {
        throw new NotImplementedException();
    }
}