using Microsoft.EntityFrameworkCore;

namespace Nova.HRIS;

public static class DbSet_Extensions
{
    public static async Task<bool> Exists(this DbSet<Province> provinces, short id)
    {
        return await provinces
            .AsNoTracking()
            .Where(province => province.Id == id && !province.IsDeleted)
            .AnyAsync();
    }
    
    public static async Task<bool> Exists(this DbSet<CivilStatus> civilStatuses, short id)
    {
        return await civilStatuses
            .AsNoTracking()
            .Where(civilStatus => civilStatus.Id == id)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Nationality> nationalities, short id)
    {
        return await nationalities
            .AsNoTracking()
            .Where(nationality => nationality.Id == id)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Barangay> barangays, string name, short? cityId)
    {
        return await barangays
            .AsNoTracking()
            .Where(barangay => barangay.Name == name && barangay.CityId == cityId && !barangay.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<City> cities, short id)
    {
        return await cities
            .AsNoTracking()
            .Where(city => city.Id == id && !city.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Employee> employees, string firstName, string middleName, string lastName, string nameSuffix, bool? sex, DateTime? birthDate)
    {
        return await employees
            .AsNoTracking()
            .Where(employee =>
                employee.FirstName == firstName
                && employee.MiddleName == middleName
                && employee.LastName == lastName
                && employee.NameSuffix == nameSuffix
                && employee.Sex == sex
                && employee.BirthDate.Value.Date == birthDate.Value.Date
                && !employee.IsDeleted
            )
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<City> cities, string name, short? provinceId)
    {
        return await cities
            .AsNoTracking()
            .Where(city => city.Name == name && city.ProvinceId == provinceId && !city.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Province> provinces, string name)
    {
        return await provinces
            .AsNoTracking()
            .Where(province => province.Name == name && !province.IsDeleted)
            .AnyAsync();
    }
}