namespace Nova.Identity;

public static class DbSetExtensions
{
    public static async Task<bool> Exists(this DbSet<User> users, int id)
    {
        return await users.AsNoTracking()
            .Where(user => user.Id == id && !user.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> UsernameExists(this DbSet<User> users, string username)
    {
        return await users.AsNoTracking()
            .Where(user => user.Username == username && !user.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<UserStatus> userStatuses, short id)
    {
        return await userStatuses.AsNoTracking()
            .Where(userStatus => userStatus.Id == id)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<UserPasswordLogin> logins, int userId)
    {
        return await logins.AsNoTracking()
            .Where(login => login.UserId == userId && !login.IsDeleted)
            .AnyAsync();
    }
}