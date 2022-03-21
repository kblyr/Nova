using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public class UserApplicationDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<UserApplicationDbContext> _entityTypeConfigurationContainingAssemblyProvider;

    public DbSet<User> Users => Set<User>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<UserApplication> UserApplications => Set<UserApplication>();

    public UserApplicationDbContext(DbContextOptions<UserApplicationDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<UserApplicationDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}
