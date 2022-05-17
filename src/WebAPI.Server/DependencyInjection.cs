using Microsoft.Extensions.DependencyInjection;
using Nova.WebAPI.Server.Auditing;

namespace Nova.WebAPI.Server;

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
        services
            .AddScoped<IApiMediator, ApiMediator>()
            .AddScoped<IResponseMapper, ResponseMapper>()
            .AddScoped<IResponseTypeMapRegistry, ResponseTypeMapRegistry>()
            .AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection AddNovaWebAPIServer(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        services.AddRequiredServices();
        var injector = new DependencyInjector(services);
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddNovaWebAPIServer(this IServiceCollection services)
    {
        return services.AddNovaWebAPIServer(injector => injector
            .AddAuditing()
        );
    }
}