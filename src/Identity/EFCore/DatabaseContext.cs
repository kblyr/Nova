using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public class DatabaseContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<DatabaseContext> _entityTypeConfiguraionContainingAssemblyProvider;

    public DatabaseContext(DbContextOptions options, IEntityTypeConfigurationContainingAssemblyProvider<DatabaseContext> entityTypeConfiguraionContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfiguraionContainingAssemblyProvider = entityTypeConfiguraionContainingAssemblyProvider;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserStatus> UserStatuses => Set<UserStatus>();
    public DbSet<Domain> Domains => Set<Domain>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<UserApplication> UserApplications => Set<UserApplication>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfiguraionContainingAssemblyProvider.Assembly);
    }
}