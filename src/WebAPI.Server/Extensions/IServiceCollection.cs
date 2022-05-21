using System.Reflection;

namespace Nova;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMapster(this IServiceCollection services, params Assembly[] assemblies)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(assemblies);
        return services
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>();
    }
}