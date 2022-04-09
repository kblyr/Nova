namespace Nova;

public interface IDependencyInjector
{
    IServiceCollection Services { get; }
}

public abstract class DependencyInjectorBase
{
   public IServiceCollection Services { get; }

    protected DependencyInjectorBase(IServiceCollection services)
    {
        Services = services;
    }

    protected DependencyInjectorBase(IDependencyInjector parent) : this(parent.Services)
    {
    }
}

public delegate void InjectDependencies<T>(T injector) where T : IDependencyInjector;