using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.EFCore.Postgres;

public class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IDependencyInjector parent) : base(parent)
    {
    }
}

public class DbContextFactoriesDependencyInjector : DependencyInjector
{
    internal string ConnectionString { get; }
    internal Assembly? EntityTypeConfigurationContainingAssembly { get; }

    public DbContextFactoriesDependencyInjector(IDependencyInjector parent, string connectionString, Assembly? entityTypeConfigurationContainingAssembly) : base(parent)
    {
        ConnectionString = connectionString;
        EntityTypeConfigurationContainingAssembly = entityTypeConfigurationContainingAssembly;
    }
}

public static class DependencyExtensions
{
    public static EFCore.DependencyInjector Postgres(this EFCore.DependencyInjector injector, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var _injector = new DependencyInjector(injector);
        injectDependencies?.Invoke(_injector);
        return injector;
    }

    public static DependencyInjector AddDbContextFactory<T>(this DependencyInjector injector, string connectionString) where T : DbContext
    {
        injector.Services.AddDbContextFactory<T>(options => options.UseNpgsql(connectionString));
        return injector;
    }

    public static DependencyInjector AddDbContextFactory<T>(this DependencyInjector injector, string connectionString, Assembly entityTypeConfigurationContainingAssembly) where T : DbContext
    {
        injector.AddDbContextFactory<T>(connectionString);
        injector.Services.AddEntityTypeConfigurationsFrom<T>(entityTypeConfigurationContainingAssembly);
        return injector;
    }

    public static DbContextFactoriesDependencyInjector AddDbContextFactories(this DependencyInjector injector, string connectionString, Assembly? entityTypeConfigurationContainingAssembly)
    {
        return new DbContextFactoriesDependencyInjector(injector, connectionString, entityTypeConfigurationContainingAssembly);
    }

    public static DbContextFactoriesDependencyInjector For<T>(this DbContextFactoriesDependencyInjector injector) where T : DbContext
    {
        injector.AddDbContextFactory<T>(injector.ConnectionString);

        if (injector.EntityTypeConfigurationContainingAssembly is not null)
            injector.Services.AddEntityTypeConfigurationsFrom<T>(injector.EntityTypeConfigurationContainingAssembly);

        return injector;
    }
}