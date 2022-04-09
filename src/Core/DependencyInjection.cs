namespace DI.Nova;

public sealed class DependencyInjector : IDependencyInjector
{
    public IServiceCollection Services { get; }

    public DependencyInjector(IServiceCollection services)
    {
        Services = services;
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddNova(this IServiceCollection services, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(services);
        injectDependencies?.Invoke(injector);
        return services;
    }
}