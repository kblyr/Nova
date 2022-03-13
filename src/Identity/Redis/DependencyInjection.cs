using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Identity.Redis;

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

    public static DependencyInjector AddConnectionMultiplexerFactory(this DependencyInjector injector, string configuration)
    {
        injector.Services.AddSingleton(new ConnectionMultiplexerFactory(configuration));
        return injector;
    }
}