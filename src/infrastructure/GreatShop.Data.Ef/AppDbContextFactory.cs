using GreatShop.Configurations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace GreatShop.Data.Ef;

//https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-a-design-time-factory
internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var dbPath = "myapp.db";
        return new AppDbContext(Options.Create(new DbConfig()
        {
            ConnectionString = $"Data Source={dbPath}"
        }));
    }
}