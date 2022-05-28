namespace Nova.HRIS.Contexts;

public sealed class HrisDbContext : DbContextBase<HrisDbContext>
{
    public HrisDbContext(DbContextOptions<HrisDbContext> options, IEntityConfigAssemblyProvider<HrisDbContext> entityConfigAssemblyProvider) : base(options, entityConfigAssemblyProvider)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
}
