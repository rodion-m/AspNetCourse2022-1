using Microsoft.EntityFrameworkCore;

namespace Lesson21.UoW.Data;

public class AppDbContext : DbContext
{
    // public DbSet<Order> Orders => Set<Order>();
    // public DbSet<Account> Accounts => Set<Account>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}