using Microsoft.EntityFrameworkCore;

namespace Nova.HRIS;

public class BarangayDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<BarangayDbContext> _entityTypeConfigurationAssemblyProvider;

    public BarangayDbContext(DbContextOptions options, IEntityTypeConfigurationContainingAssemblyProvider<BarangayDbContext> entityTypeConfigurationAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationAssemblyProvider = entityTypeConfigurationAssemblyProvider;
    }

    public DbSet<Barangay> Barangays => Set<Barangay>();
    public DbSet<City> Cities => Set<City>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationAssemblyProvider.Assembly);
    }
}