using Microsoft.Extensions.DependencyInjection;
using Nova.Core.Utilities;

namespace Nova.Core;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services) : base(services)
    {
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddNova(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = new DependencyInjector(services);
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddNova(this IServiceCollection services)
    {
        return services.AddNova(injector => injector
            .AddUtilities()
        );
    }
}
