using System.Reflection;
using System.Security.Cryptography;
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
    static IServiceCollection AddRequiredServices(this IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
    {
        services
            .AddScoped<IApiMediator, ApiMediator>()
            .AddScoped<IResponseMapper, ResponseMapper>()
            .AddScoped<IResponseTypeMapRegistry, ResponseTypeMapRegistry>()
            .AddScoped<ResponseTypeMapRegistry.AssemblyScanner>(sp => new(assembliesToScan))
            .AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection AddNovaWebAPIServer(this IServiceCollection services, IEnumerable<Assembly> assembliesToScan, InjectDependencies<DependencyInjector> injectDependencies)
    {
        services.AddRequiredServices(assembliesToScan);
        var injector = new DependencyInjector(services);
        injector.AddAuditing();
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddNovaWebAPIServer(this IServiceCollection services, params Assembly[] assembliesToScan)
    {
        return services.AddNovaWebAPIServer(assembliesToScan, injector => {});
    }

    public static DependencyInjector AddHashIdConverter<T>(this DependencyInjector injector, string salt = "", int minHashLength = 0) where T : IHashIdConverter
    {
        var t = typeof(T);
        var converter = Activator.CreateInstance(t, salt, minHashLength);
        
        if (converter is not null)
        {
            injector.Services.AddSingleton(t, sp => converter);
        }

        return injector;
    }
}