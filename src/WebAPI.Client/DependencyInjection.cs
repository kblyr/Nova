using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.WebAPI.Client;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IDependencyInjector parent) : base(parent)
    {
    }
}

public static class DependencyExtensions
{
    public static Shared.DependencyInjector Client(this Shared.DependencyInjector injector, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var _injector = new DependencyInjector(injector);
        injectDependencies?.Invoke(_injector);
        return injector;
    } 

    public static DependencyInjector AddApiResponseSerializer(this DependencyInjector injector, params Assembly[] assembliesToScan)
    {
        injector.Services
            .AddSingleton<IApiResponseSerializer, ApiResponseSerializer>()
            .AddSingleton<IApiResponseTypeRegistry, ApiResponseTypeRegistry>()
            .AddSingleton<ApiResponseTypeRegistry.AssemblyScanner>(provider => new ApiResponseTypeRegistry.AssemblyScanner(assembliesToScan));
        return injector;
    }
}
