using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public class UserStatusDbContext : DbContext
{
    readonly IEntityTypeConfigurationContainingAssemblyProvider<UserStatusDbContext> _entityTypeConfigurationContainingAssemblyProvider;

    public UserStatusDbContext(DbContextOptions<UserStatusDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<UserStatusDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options)
    {
        _entityTypeConfigurationContainingAssemblyProvider = entityTypeConfigurationContainingAssemblyProvider;
    }
    
    public DbSet<UserStatus> UserStatuses => Set<UserStatus>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_entityTypeConfigurationContainingAssemblyProvider.Assembly);
    }
}