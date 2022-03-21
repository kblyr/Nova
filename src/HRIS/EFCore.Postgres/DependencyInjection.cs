using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.HRIS.EFCore.Postgres;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services, Assembly[]? assemblyMarkers) : base(services, assemblyMarkers)
    {
    }
}

public static class DependencyExtensions
{
    public static EFCore.DependencyInjector Postgres(this EFCore.DependencyInjector parentInjector, Action<DependencyInjector>? injectDependencies = null)
    {
        var injector = new DependencyInjector(parentInjector.Services, parentInjector.AssemblyMarkers);
        injectDependencies?.Invoke(injector);
        return parentInjector;
    }

    public static DependencyInjector AddDefault(this DependencyInjector injector)
    {
        injector.Services
            .AddEntityTypeConfigurationContainingAssemblyProvider<EntityTypeConfigurationContainingAssemblyProvider, BarangayDbContext>()
            .AddEntityTypeConfigurationContainingAssemblyProvider<EntityTypeConfigurationContainingAssemblyProvider, CityDbContext>()
            .AddEntityTypeConfigurationContainingAssemblyProvider<EntityTypeConfigurationContainingAssemblyProvider, EmployeeDbContext>()
            .AddEntityTypeConfigurationContainingAssemblyProvider<EntityTypeConfigurationContainingAssemblyProvider, ProvinceDbContext>();
        return injector;
    }
}