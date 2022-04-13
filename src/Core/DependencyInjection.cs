using System.Reflection;

namespace Nova.Core;

public sealed class DependencyInjector : IDependencyInjector
{
    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }

    public DependencyInjector(IServiceCollection services, IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;
    }
}

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}

public static class DependencyExtensions
{
    public static IServiceCollection Nova(this IServiceCollection services, IConfiguration configuration, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(services, configuration);
        injectDependencies?.Invoke(injector);
        return services;
    }
}