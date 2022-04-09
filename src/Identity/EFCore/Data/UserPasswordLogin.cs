namespace Nova.Identity.Data;

public sealed class UserPasswordLoginDbContext : DbContextBase<UserPasswordLoginDbContext>
{
    public UserPasswordLoginDbContext(DbContextOptions<UserPasswordLoginDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<UserPasswordLoginDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options, entityTypeConfigurationContainingAssemblyProvider)
    {
    }

    public DbSet<UserPasswordLogin> UserPasswordLogins => Set<UserPasswordLogin>();
    public DbSet<User> Users => Set<User>();
}
