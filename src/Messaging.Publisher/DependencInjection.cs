using Microsoft.Extensions.DependencyInjection;

namespace Nova.Messaging.Publisher;

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
        services.AddScoped<IBusAdapter, BusAdapter>();
        return services;
    }

    public static IServiceCollection AddNovaMessagingPublisher(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        services.AddRequiredServices();
        var injector = new DependencyInjector(services);
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddNovaMessagingPublisher(this IServiceCollection services)
    {
        return services.AddNovaMessagingPublisher(injector => {
            injector.Services
                .AddScoped<IPublishFailureHandler, LogPublishFailureHandler>();
        });
    }

    public static DependencyInjector AddPublishFailureDirectory(this DependencyInjector injector, string directory)
    {
        injector.Services.AddScoped<IPublishFailureHandler>(sp => new SaveToFilePublishFailureHandler(directory));
        return injector;
    }
}