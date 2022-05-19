namespace Nova.Identity.Contexts;

public sealed class IdentityDbContext : DbContextBase<IdentityDbContext>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }
}