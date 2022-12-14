using GreatShop.Data.Ef;
using GreatShop.Domain.Repositories;
using GreatShop.Domain.Services;
using GreatShop.Infrastructure;
using GreatShop.WebApi.Mappers;
using GreatShop.WebApi.Services;

namespace GreatShop.WebApi.Extensions;

public static class AddDomainDependenciesExtension
{
    public static IServiceCollection AddDomainDependencies(
        this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactoryEf>();
        //services.AddScoped<IUnitOfWork>(provider => provider.GetService<IUnitOfWorkFactory>().CreateAsync());
        //services.AddRepository<IProductRepository>(uow => uow.ProductRepository);
    
        services.AddSingleton<IClock, UtcClock>();
        services.AddSingleton<IPasswordHasherService, Pbkdf2PasswordHasher>();
        services.AddSingleton<HttpModelsMapper>();

        services.AddScoped<CatalogService>();
        services.AddScoped<AccountService>();
        
        return services;
    }
}