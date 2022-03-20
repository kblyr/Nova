using Microsoft.EntityFrameworkCore;

namespace Nova.HRIS;

public class CityDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<CityDbContext> _entityTypeConfigurationContainingAssemblyProvider;

    public CityDbContext(DbContextOptions options, IEntityTypeConfigurationContainingAssemblyProvider<CityDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }

    public DbSet<City> Cities => Set<City>();
    public DbSet<Province> Provinces => Set<Province>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}