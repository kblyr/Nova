using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.HRIS.Core;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services, Assembly[]? assemblyMarkers) : base(services, assemblyMarkers)
    {
    }
}

public static class DependencyExtensions
{
    public static Nova.Core.DependencyInjector HRIS(this Nova.Core.DependencyInjector parentInjector, Action<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(parentInjector.Services, parentInjector.AssemblyMarkers);
        injectDependencies?.Invoke(injector);
        return parentInjector;
    }
}