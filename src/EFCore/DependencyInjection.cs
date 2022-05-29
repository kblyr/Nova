using System.Reflection;

namespace Nova.EFCore;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services) : base(services)
    {
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddNovaEFCore(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = new DependencyInjector(services);
        injectDependencies(injector);
        return services;
    }

    public static DependencyInjector AddDbContextFactory<T>(this DependencyInjector injector, Action<DbContextOptionsBuilder>? optionsAction = null) where T : DbContext
    {
        injector.Services.AddDbContextFactory<T>(optionsAction);
        return injector;
    }

    public static DependencyInjector AddDbContextFactory<T>(this DependencyInjector injector, Assembly entityConfigAssembly, Action<DbContextOptionsBuilder>? optionsAction = null) where T : DbContext
    {
        injector.AddDbContextFactory<T>(optionsAction);
        injector.Services.AddSingleton<IEntityConfigAssemblyProvider<T>>(sp => new EntityConfigAssemblyProvider<T>(entityConfigAssembly));
        return injector;
    }
}