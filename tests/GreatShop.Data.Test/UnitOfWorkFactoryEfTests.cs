using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GreatShop.Data.Ef;
using GreatShop.Data.MongoDb;
using GreatShop.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MongoDB.Driver;
using Xunit;

namespace GreatShop.Data.Test;

public class UnitOfWorkFactoryEfTests : UnitOfWorkFactoryTests
{
    private readonly SqliteConnection _connection;

    public UnitOfWorkFactoryEfTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _unitOfWorkFactory = new UnitOfWorkFactoryEf(new FakeDbContextFactory(options));
    }
    
    public override void Dispose()
    {
        _connection.Dispose();
    }
    
    private class FakeDbContextFactory : IDbContextFactory<AppDbContext>
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public FakeDbContextFactory(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public AppDbContext CreateDbContext()
        {
            var context = new AppDbContext(_options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
