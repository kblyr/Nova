using Microsoft.Extensions.DependencyInjection;
using Nova.Utilities;

namespace Nova.Core.Utilities;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services) : base(services)
    {
    }
}

public static class DependencyExtensions
{
    public static Core.DependencyInjector AddUtilities(this Core.DependencyInjector injector, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var _injector = new DependencyInjector(injector.Services);
        injectDependencies(_injector);
        return injector;
    }

    public static Core.DependencyInjector AddUtilities(this Core.DependencyInjector injector)
    {
        return injector.AddUtilities(injector => injector.Services
            .AddSingleton<IRandomStringGenerator, RandomStringGenerator>()
        );
    }
}