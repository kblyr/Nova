using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public class AccessTokenDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<AccessTokenDbContext> _entityTypeConfigurationContainingAssemblyProvider;

    public AccessTokenDbContext(DbContextOptions<AccessTokenDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<AccessTokenDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<UserApplication> UserApplications => Set<UserApplication>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}
