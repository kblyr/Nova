using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Core;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services, Assembly[]? assemblyMarkers) : base(services, assemblyMarkers)
    {
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddNova(this IServiceCollection services, Action<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(services, null);
        injectDependencies?.Invoke(injector);
        return services;
    }

    public static IServiceCollection AddNova(this IServiceCollection services, Assembly[] assemblyMarkers, Action<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(services, assemblyMarkers);
        injectDependencies?.Invoke(injector);
        return services;
    }
}