using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Identity.Core;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IDependencyInjector parent) : base(parent)
    {
    }
}

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}

public static class DependencyExtensions
{
    public static Nova.Core.DependencyInjector Identity(this Nova.Core.DependencyInjector injector, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var _injector = new DependencyInjector(injector);
        injectDependencies?.Invoke(_injector);
        return injector;
    }

    public static DependencyInjector Configure<T>(this DependencyInjector injector, string configKey) where T : class
    {
        injector.Services.Configure<T>(injector.Configuration.GetSection(configKey));
        return injector;
    }
}