namespace Nova.HRIS;

public static class DbSetExtensions
{
    public static async Task<bool> DoesExists(this DbSet<Employee> employees, string firstName, string lastName, DateTime birthDate, CancellationToken cancellationToken = default)
    {
        return await employees.AsNoTracking()
            .Where(employee => 
                !employee.IsDeleted
                && employee.FirstName == firstName
                && employee.LastName == lastName
                && employee.BirthDate.Date == birthDate.Date
            )
            .AnyAsync(cancellationToken);
    }

    public static async Task<bool> DoesExists(this DbSet<CivilStatus> civilStatuses, short id, CancellationToken cancellationToken = default)
    {
        return await civilStatuses.AsNoTracking()
            .Where(civilStatus => civilStatus.Id == id)
            .AnyAsync(cancellationToken);
    }

    public static async Task<bool> DoesExists(this DbSet<EmploymentType> employmentTypes, short id, CancellationToken cancellationToken = default)
    {
        return await employmentTypes.AsNoTracking()
            .Where(employmentType => employmentType.Id == id)
            .AnyAsync(cancellationToken);
    }

    public static async Task<bool> DoesExists(this DbSet<Citizenship> citizenships, int id, CancellationToken cancellationToken = default)
    {
        return await citizenships.AsNoTracking()
            .Where(citizenship => citizenship.Id == id && !citizenship.IsDeleted)
            .AnyAsync(cancellationToken);
    }

    public static async Task<bool> DoesExists(this DbSet<EmploymentStatus> employmentStatuses, short id, CancellationToken cancellationToken = default)
    {
        return await employmentStatuses.AsNoTracking()
            .Where(employmentStatus => employmentStatus.Id == id)
            .AnyAsync(cancellationToken);
    }

    public static async Task<bool> DoesExists(this DbSet<Office> offices, int id, CancellationToken cancellationToken = default)
    {
        return await offices.AsNoTracking()
            .Where(office => office.Id == id && !office.IsDeleted)
            .AnyAsync(cancellationToken);
    }

    public static async Task<bool> DoesExists(this DbSet<Position> positions, int id, CancellationToken cancellationToken = default)
    {
        return await positions.AsNoTracking()
            .Where(position => position.Id == id && !position.IsDeleted)
            .AnyAsync(cancellationToken);
    }
}