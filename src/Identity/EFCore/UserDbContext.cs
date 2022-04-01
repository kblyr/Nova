using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public class UserDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<UserDbContext> _entityTypeConfigurationContainingAssemblyProvider;

    public UserDbContext(DbContextOptions<UserDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<UserDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserStatus> UserStatuses => Set<UserStatus>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<UserApplication> UserApplications => Set<UserApplication>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}
