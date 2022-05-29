using Microsoft.Extensions.DependencyInjection;

namespace Nova.WebAPI;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services) : base(services)
    {
    }
}

public static class DependencyExtensions
{
    static IServiceCollection AddRequiredServices(this IServiceCollection services)
    {
        services.AddSingleton<IApiResponseTypeRegistryKeyProvider, ApiResponseTypeRegistryKeyProvider>();
        return services; 
    }

    public static IServiceCollection AddNovaWebAPI(this IServiceCollection services)
    {
        services.AddRequiredServices();
        return services;
    }
}
