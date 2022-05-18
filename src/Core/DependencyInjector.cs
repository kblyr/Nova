using Microsoft.Extensions.DependencyInjection;

namespace Nova;

public interface IDependencyInjector
{
    IServiceCollection Services { get; }
}

public abstract class DependencyInjectorBase
{
    readonly IServiceCollection _services;

    protected DependencyInjectorBase(IServiceCollection services)
    {
        _services = services;
    }

    public IServiceCollection Services => _services;
}

public delegate void InjectDependencies<TInjector>(TInjector injector) where TInjector : IDependencyInjector;