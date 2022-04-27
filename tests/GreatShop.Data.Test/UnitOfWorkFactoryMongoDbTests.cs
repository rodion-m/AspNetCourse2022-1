using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GreatShop.Data.MongoDb;
using GreatShop.Domain.Entities;
using MongoDB.Driver;
using Xunit;

namespace GreatShop.Data.Test;

public class UnitOfWorkFactoryMongoDbTests : UnitOfWorkFactoryTests
{
    private const string DbName = "db_test";
    private readonly MongoClient _client;
    
    public UnitOfWorkFactoryMongoDbTests()
    {
        var connectionString = Environment.GetEnvironmentVariable("mongodb_connection_string")!;
        ArgumentNullException.ThrowIfNull(connectionString);
        _client = new MongoClient(connectionString);
        _unitOfWorkFactory = new UnitOfWorkFactoryMongoDb(_client, DbName);
    }
    
    public override void Dispose()
    {
        // Очищаем тестовую базу после завершения всех тестов
        _client.DropDatabase(DbName);
    }
}