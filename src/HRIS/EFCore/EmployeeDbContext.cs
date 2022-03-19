using Microsoft.EntityFrameworkCore;

namespace Nova.HRIS;

public class EmployeeDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<EmployeeDbContext> _entityTypeConfigurationAssemblyProvider;

    public EmployeeDbContext(DbContextOptions options, IEntityTypeConfigurationContainingAssemblyProvider<EmployeeDbContext> entityTypeConfigurationAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationAssemblyProvider = entityTypeConfigurationAssemblyProvider;
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<CivilStatus> CivilStatuses => Set<CivilStatus>();
    public DbSet<Nationality> Nationalities => Set<Nationality>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Barangay> Barangays => Set<Barangay>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationAssemblyProvider.Assembly);
    }
}