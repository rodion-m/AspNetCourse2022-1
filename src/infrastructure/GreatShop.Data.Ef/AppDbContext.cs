using System.Reflection;
using GreatShop.Configurations;
using GreatShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GreatShop.Data.Ef;

public class AppDbContext : DbContext
{
    private readonly DbConfig? _config;
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(IOptions<DbConfig> config)
    {
        if (config == null) throw new ArgumentNullException(nameof(config));
        _config = config.Value;
    }
    
    internal AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_config?.ConnectionString is {} connectionString)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        if (_config == null) return;
        
        if (_config.DisableQueriesLogging)
        {
            optionsBuilder.UseLoggerFactory(CreateEmptyLoggerFactory());
        }
        else
        {
            if (_config.EnableDetailedErrors)
                optionsBuilder.EnableDetailedErrors();
            if (_config.EnableSensitiveDataLogging)
                optionsBuilder.EnableSensitiveDataLogging();
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