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
}