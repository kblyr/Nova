using Microsoft.Extensions.DependencyInjection;
using Nova.Identity.Core.Security;

namespace Nova.Identity.Core;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services) : base(services)
    {
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddNovaIdentity(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = new DependencyInjector(services);
        injector
            .AddSecurity();
        injectDependencies(injector);
        return services;
    }

    public static IServiceCollection AddNovaIdentity(this IServiceCollection services)
    {
        return services.AddNovaIdentity(injector => {});
    }
}