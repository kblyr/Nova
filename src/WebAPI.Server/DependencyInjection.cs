using Microsoft.Extensions.DependencyInjection;

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

    public static IServiceCollection AddNovaWebAPIServer(this IServiceCollection services)
    {
        return services;
    }
}