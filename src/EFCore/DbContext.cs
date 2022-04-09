using Microsoft.EntityFrameworkCore;

namespace Nova;

public abstract class DbContextBase<T> : DbContext where T : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<T> _entityTypeConfigurationContainingAssemblyProvider;

    protected DbContextBase(DbContextOptions<T> options, IEntityTypeConfigurationContainingAssemblyProvider<T> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}
