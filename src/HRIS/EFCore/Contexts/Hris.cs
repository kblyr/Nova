namespace Nova.HRIS.Contexts;

public sealed class HRISDbContext : DbContextBase<HRISDbContext>
{
    public HRISDbContext(DbContextOptions<HRISDbContext> options, IEntityConfigAssemblyProvider<HRISDbContext> entityConfigAssemblyProvider) : base(options, entityConfigAssemblyProvider)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
}
