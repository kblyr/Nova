namespace Nova.Identity.Contexts;

public sealed class IdentityDbContext : DbContextBase<IdentityDbContext>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IEntityConfigAssemblyProvider<IdentityDbContext> entityConfigAssemblyProvider) : base(options, entityConfigAssemblyProvider)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserStatus> UserStatuses => Set<UserStatus>();
}