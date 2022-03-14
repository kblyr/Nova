using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nova.Identity.Configuration;

namespace Nova.Identity.Core;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services, Assembly[]? assemblyMarkers) : base(services, assemblyMarkers)
    {
    }
}

public static class DependencyExtensions
{
    public static Nova.Core.DependencyInjector Identity(this Nova.Core.DependencyInjector parentInjector, Action<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(parentInjector.Services, parentInjector.AssemblyMarkers);
        injectDependencies?.Invoke(injector);
        return parentInjector;
    }

    public static DependencyInjector SetupConfigurations(this DependencyInjector injector, IConfiguration configuration)
    {
        injector.Services
            .Configure<AccessTokenConfig>(configuration.GetSection(AccessTokenConfig.ConfigKey));
        return injector;
    }
}