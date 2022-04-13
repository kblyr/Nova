using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nova;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresDbContextFactory<T>(this IServiceCollection services, string connectionString) where T : DbContext
    {
        return services.AddDbContextFactory<T>(options => options.UseNpgsql(connectionString));
    }

    public static IServiceCollection AddPostgresDbContextFactory<T>(this IServiceCollection services, string connectionString, Assembly assembly) where T : DbContext
    {
        return services
            .AddPostgresDbContextFactory<T>(connectionString)
            .AddEntityTypeConfigurationsFrom<T>(assembly);
    }
}