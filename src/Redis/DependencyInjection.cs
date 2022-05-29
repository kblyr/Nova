using Microsoft.Extensions.DependencyInjection;

namespace Nova.Redis;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services) : base(services)
    {
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddNovaRedis(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = new DependencyInjector(services);
        injectDependencies(injector);
        return services;
    }

    public static DependencyInjector AddMultiplexerProvider<T>(this DependencyInjector injector, string configuration) where T : IMultiplexerProvider
    {
        var t = typeof(T);
        var ctorInfo  = t.GetConstructor(new[] { typeof(string) });

        var mp = ctorInfo is null ? Activator.CreateInstance(t) : Activator.CreateInstance(t, configuration);

        if (mp is not null)
        {
            injector.Services.AddScoped(t, sp => mp);
        }

        return injector;
    }

    public static DependencyInjector AddKeyGenerator<T>(this DependencyInjector injector) where T : class, IKeyGenerator
    {
        injector.Services.AddScoped<T>();
        return injector;
    }
}