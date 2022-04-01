using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public class PermissionDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<PermissionDbContext> _entityTypeConfigurationContainingAssemblyProvider;

    public PermissionDbContext(DbContextOptions<PermissionDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<PermissionDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }

    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Domain> Domains => Set<Domain>();
    public DbSet<Application> Applications => Set<Application>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}
