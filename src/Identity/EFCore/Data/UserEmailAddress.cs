namespace Nova.Identity.Data;

public sealed class UserEmailAddressDbContext : DbContextBase<UserEmailAddressDbContext>
{
    public UserEmailAddressDbContext(DbContextOptions<UserEmailAddressDbContext> options, IEntityTypeConfigurationContainingAssemblyProvider<UserEmailAddressDbContext> entityTypeConfigurationContainingAssemblyProvider) : base(options, entityTypeConfigurationContainingAssemblyProvider)
    {
    }

    public DbSet<UserEmailAddress> UserEmailAddresses => Set<UserEmailAddress>();
    public DbSet<User> Users => Set<User>();
}
