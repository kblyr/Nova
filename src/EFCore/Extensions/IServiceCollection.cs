using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nova;

public static class IServiceCollection_Extensions
{
    public static IServiceCollection AddEntityTypeConfigurationContainingAssemblyProvider<T, TDbContext>(this IServiceCollection services) 
        where T : class, IEntityTypeConfigurationContainingAssemblyProvider<TDbContext>
        where TDbContext : DbContext
    {
        services.AddSingleton<IEntityTypeConfigurationContainingAssemblyProvider<TDbContext>, T>();
        return services;
    }
}