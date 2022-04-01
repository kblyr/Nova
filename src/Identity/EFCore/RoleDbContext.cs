using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public class RoleDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<RoleDbContext> _entityTypeConfigurationContainingAssemblyProvider;

    public RoleDbContext(DbContextOptions<RoleDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<RoleDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Domain> Domains => Set<Domain>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Permission> Permissions => Set<Permission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}
