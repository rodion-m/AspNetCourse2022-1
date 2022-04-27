using System.Reflection;
using GreatShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GreatShop.Data.Ef;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Product> Products => Set<Product>();
    
    private readonly string? _connectionString;
    private readonly bool? _enableQueriesLogging;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    internal AppDbContext(string? connectionString, bool enableQueriesLogging)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _enableQueriesLogging = enableQueriesLogging;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_connectionString != null)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        if (_enableQueriesLogging is true)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else if (_enableQueriesLogging is false)
        {
            optionsBuilder
                .UseLoggerFactory(CreateEmptyLoggerFactory());
        }
    }

    private static ILoggerFactory CreateEmptyLoggerFactory()
    {
        return LoggerFactory.Create(builder => builder.AddFilter((_, _) => false));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        BuildCartItems(modelBuilder);
        modelBuilder.Entity<Account>()
            .Property(e => e.Roles)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            );
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