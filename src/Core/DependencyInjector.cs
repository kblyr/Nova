namespace Nova;

public interface IDependencyInjector
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
}

public abstract class DependencyInjectorBase
{
   public IServiceCollection Services { get; }
   public IConfiguration Configuration { get; }

    protected DependencyInjectorBase(IServiceCollection services, IConfiguration configuration)
    {
        Services = services;
        Configuration = configuration;
    }

    protected DependencyInjectorBase(IDependencyInjector parent) : this(parent.Services, parent.Configuration)
    {
    }
}

public delegate void InjectDependencies<T>(T injector) where T : IDependencyInjector;