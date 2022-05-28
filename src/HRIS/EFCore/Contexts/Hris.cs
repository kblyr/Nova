namespace Nova.HRIS.Contexts;

public sealed class HRISDbContext : DbContextBase<HRISDbContext>
{
    public HRISDbContext(DbContextOptions<HRISDbContext> options, IEntityConfigAssemblyProvider<HRISDbContext> entityConfigAssemblyProvider) : base(options, entityConfigAssemblyProvider)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeeAddress> EmployeeAddresses => Set<EmployeeAddress>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Barangay> Barangays => Set<Barangay>();
    public DbSet<EmploymentType> EmploymentTypes => Set<EmploymentType>();
    public DbSet<CivilStatus> CivilStatuses => Set<CivilStatus>();
    public DbSet<Citizenship> Citizenships => Set<Citizenship>();
    public DbSet<EmploymentStatus> EmploymentStatuses => Set<EmploymentStatus>();
    public DbSet<Office> Offices => Set<Office>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Employment> Employments => Set<Employment>();
    public DbSet<EmployeeSalaryGradeStep> EmployeeSalaryGradeSteps => Set<EmployeeSalaryGradeStep>();
    public DbSet<SalaryGradeStep> SalaryGradeSteps => Set<SalaryGradeStep>();
}
