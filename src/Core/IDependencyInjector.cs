using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova;

public interface IDependencyInjector
{
    IServiceCollection Services { get; }
    public Assembly[] AssemblyMarkers { get; }
}

public abstract class DependencyInjectorBase
{
    public IServiceCollection Services { get; }
    public Assembly[] AssemblyMarkers { get; }

    protected DependencyInjectorBase(IServiceCollection services, Assembly[]? assemblyMarkers)
    {
        Services = services;
        AssemblyMarkers = assemblyMarkers ?? Array.Empty<Assembly>();
    }
}