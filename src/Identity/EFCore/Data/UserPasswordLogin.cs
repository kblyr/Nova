namespace Nova.Identity.Data;

public sealed class UserPasswordLoginDbContext : DbContext
{
    public UserPasswordLoginDbContext(DbContextOptions<UserPasswordLoginDbContext> options) : base(options)
    {
    }

    public DbSet<UserPasswordLogin> UserPasswordLogins => Set<UserPasswordLogin>();
    public DbSet<User> Users => Set<User>();
}
