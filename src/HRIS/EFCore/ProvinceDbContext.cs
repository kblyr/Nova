using Microsoft.EntityFrameworkCore;

namespace Nova.HRIS;

public class ProvinceDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<ProvinceDbContext> _entityTypeConfigurationContainingAssemblyProvider;

    public ProvinceDbContext(DbContextOptions options, IEntityTypeConfigurationContainingAssemblyProvider<ProvinceDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }

    public DbSet<Province> Provinces => Set<Province>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}