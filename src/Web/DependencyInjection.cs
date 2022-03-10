using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Web;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services, Assembly[]? assemblyMarkers) : base(services, assemblyMarkers)
    {
    }
}

public static class DependencyExtensions
{
    public static Nova.Core.DependencyInjector Web(this Nova.Core.DependencyInjector parentInjector, Action<DependencyInjector>? injectDependencies = null)
    {
        return parentInjector.Web(parentInjector.AssemblyMarkers, injectDependencies);
    }
    public static Nova.Core.DependencyInjector Web(this Nova.Core.DependencyInjector parentInjector, Assembly[] assemblyMarkers, Action<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(parentInjector.Services, assemblyMarkers);
        injectDependencies?.Invoke(injector);
        return parentInjector;
    }
}