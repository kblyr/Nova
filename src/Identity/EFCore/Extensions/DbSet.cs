using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public static class DbSet_Extensions
{
    public static async Task<bool> UsernameExists(this DbSet<User> users, string username)
    {
        return await users
            .AsNoTracking()
            .Where(user => user.Username == username && !user.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<UserStatus> userStatuses, short id)
    {
        return await userStatuses
            .AsNoTracking()
            .Where(userStatus => userStatus.Id == id)
            .AnyAsync();
    }
}