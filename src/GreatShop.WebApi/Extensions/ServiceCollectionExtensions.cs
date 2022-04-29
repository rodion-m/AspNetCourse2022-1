using GreatShop.Domain.Repositories;

namespace GreatShop.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepository<TRepository>(
        this IServiceCollection services,
        Func<IUnitOfWork, TRepository> implementationFactory)
        where TRepository : class
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (implementationFactory == null) throw new ArgumentNullException(nameof(implementationFactory));
        return services.AddScoped<TRepository>(provider =>
        {
            var unitOfWork = provider.GetService<IUnitOfWork>();
            if (unitOfWork == null)
            {
                throw new InvalidOperationException($"{nameof(IUnitOfWork)} is not registered!");
            }
            return implementationFactory(unitOfWork);
        });
    }
}