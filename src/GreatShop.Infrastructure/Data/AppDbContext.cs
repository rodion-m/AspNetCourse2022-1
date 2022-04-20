using System.Reflection;
using GreatShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreatShop.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Product> Products => Set<Product>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        BuildCartItems(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private void BuildCartItems(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(action =>
        {
            action.HasOne(dto => dto.Cart)
                .WithMany(dto => dto.Items)
                .IsRequired();
        });
    }
}