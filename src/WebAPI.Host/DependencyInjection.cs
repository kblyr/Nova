using System.Reflection;

namespace Nova.WebAPI.Host;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IDependencyInjector parent) : base(parent)
    {
    }
}

public static class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}

public static class DependencyExtensions
{
    public static Shared.DependencyInjector Host(this Shared.DependencyInjector injector, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var _injector = new DependencyInjector(injector).Default();
        injectDependencies?.Invoke(_injector);
        return injector;
    }

    static DependencyInjector Default(this DependencyInjector injector)
    {
        injector.Services
            .AddHttpContextAccessor();
        return injector;
    }

    public static DependencyInjector AddApiMediator(this DependencyInjector injector)
    {
        injector.Services.AddSingleton<IApiMediator, ApiMediator>();
        return injector;
    }

    public static DependencyInjector AddResponseMapping(this DependencyInjector injector, params Assembly[] assembliesToScan)
    {
        injector.Services
            .AddSingleton<IResponseMapper, ResponseMapper>()
            .AddSingleton<IResponseTypeMapRegistry, ResponseTypeMapRegistry>()
            .AddSingleton<ResponseTypeMapRegistry.AssemblyScanner>(provider => new ResponseTypeMapRegistry.AssemblyScanner(assembliesToScan));
        return injector;
    }
}