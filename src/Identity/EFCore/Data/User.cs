namespace Nova.Identity.Data;

public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserStatus> UserStatuses => Set<UserStatus>();
    public DbSet<UserEmailAddress> UserEmailAddresses => Set<UserEmailAddress>();
}