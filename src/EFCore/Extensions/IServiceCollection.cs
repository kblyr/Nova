using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nova;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddEntityTypeConfigurationsFrom<T>(this IServiceCollection services, Assembly assembly) where T : DbContext
    {
        return services.AddSingleton<IEntityTypeConfigurationContainingAssemblyProvider<T>>(provider => new EntityTypeConfigurationContainingAssemblyProvider<T>(assembly));
    }
}