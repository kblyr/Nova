using Microsoft.Extensions.DependencyInjection;

namespace Nova.WebAPI.Shared;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IDependencyInjector parent) : base(parent)
    {
    }
}

public static class DependencyExtensions
{
    public static Core.DependencyInjector WebAPI(this Core.DependencyInjector injector, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var _injector = new DependencyInjector(injector).Default();
        injectDependencies?.Invoke(_injector);
        return injector;
    }

    static DependencyInjector Default(this DependencyInjector injector)
    {
        injector.Services
            .AddSingleton<IApiResponseTypeRegistryKeyProvider, ApiResponseTypeRegistryKeyProvider>();
        return injector;
    }
}
