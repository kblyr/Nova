namespace Nova.EFCore;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IDependencyInjector parent) : base(parent)
    {
    }
}

public static class DependencyExtensions
{
    public static Core.DependencyInjector EFCore(this Core.DependencyInjector injector, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var _injector = new DependencyInjector(injector);
        injectDependencies?.Invoke(_injector);
        return injector;
    }
}