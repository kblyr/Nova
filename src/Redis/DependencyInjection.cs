using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Redis;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services, Assembly[]? assemblyMarkers) : base(services, assemblyMarkers)
    {
    }
}

public static class DependencyExtensions
{
    public static Core.DependencyInjector Redis(this Core.DependencyInjector parentInjector, Action<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(parentInjector.Services, parentInjector.AssemblyMarkers);
        injectDependencies?.Invoke(injector);
        return parentInjector;
    }

    public static DependencyInjector AddMultiplexerProvider<TMultiplexerProvider>(this DependencyInjector injector, string configuration) where TMultiplexerProvider : MultiplexerProviderBase
    {
        var type = typeof(TMultiplexerProvider);
        var instance = Activator.CreateInstance(type, configuration);

        if (instance is not null)
            injector.Services.AddSingleton(type, instance);

        return injector;
    }
}