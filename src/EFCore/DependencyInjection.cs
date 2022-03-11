using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.EFCore;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services, Assembly[]? assemblyMarkers) : base(services, assemblyMarkers)
    {
    }
}

public static class DependencyExtensions
{
    public static Core.DependencyInjector EFCore(this Core.DependencyInjector parentInjector, Action<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(parentInjector.Services, parentInjector.AssemblyMarkers);
        injectDependencies?.Invoke(injector);
        return parentInjector;
    }

    public static DependencyInjector AddDbContextFactory<TDbContext>(this DependencyInjector injector, Action<DbContextOptionsBuilder> buildOptions) where TDbContext : DbContext
    {
        injector.Services.AddDbContextFactory<TDbContext>(buildOptions);
        return injector;
    }
}