using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using GreatShop.Data.Ef;
using GreatShop.Data.MongoDb;
using GreatShop.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MongoDB.Driver;
using Npgsql;
using Xunit;

namespace GreatShop.Data.Test;

// ReSharper disable once ClassNeverInstantiated.Global
public class EfTests : DbTests
{
    private readonly IDbConnection _connection;

    public EfTests()
    {
        var connectionString = Environment.GetEnvironmentVariable("postgres_connection_string")!;
        ArgumentNullException.ThrowIfNull(connectionString);
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql((NpgsqlConnection) _connection)
            .Options;

        var dbContextFactory = new FakeDbContextFactory(options);
        _unitOfWorkFactory = new UnitOfWorkFactoryEf(dbContextFactory);
    }

    public override void Dispose()
    {
        _connection.Dispose();
    }
    
    private class FakeDbContextFactory : IDbContextFactory<AppDbContext>, IDisposable
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly List<DbContext> _contexts = new();

        public FakeDbContextFactory(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public AppDbContext CreateDbContext()
        {
            var context = new AppDbContext(_options);
            _contexts.Add(context);
            context.Database.EnsureCreated();
            return context;
        }

        public void Dispose()
        {
            foreach (var dbContext in _contexts)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Dispose();
            }
        }
    }
}
