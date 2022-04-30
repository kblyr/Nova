namespace Nova.Identity.Data;

public sealed class UserDbContext : DbContextBase<UserDbContext>
{
    public UserDbContext(DbContextOptions<UserDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<UserDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options, entityTypeConfigurationContainingAssemblyProvider)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserStatus> UserStatuses => Set<UserStatus>();
    public DbSet<UserEmailAddress> UserEmailAddresses => Set<UserEmailAddress>();
    public DbSet<UserPasswordLogin> UserPasswordLogins => Set<UserPasswordLogin>();
}