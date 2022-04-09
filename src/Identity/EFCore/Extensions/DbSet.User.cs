namespace Nova.Identity;

public static partial class DbSetExtensions
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
}