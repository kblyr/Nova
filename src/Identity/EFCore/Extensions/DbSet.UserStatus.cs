namespace Nova.Identity;

public static partial class DbSetExtensions
{

    public static async Task<bool> Exists(this DbSet<UserStatus> userStatuses, short id)
    {
        return await userStatuses.AsNoTracking()
            .Where(userStatus => userStatus.Id == id)
            .AnyAsync();
    }
}