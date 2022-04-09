namespace Nova.Identity;

public static partial class DbSetExtensions
{
    public static async Task<bool> Exists(this DbSet<UserPasswordLogin> logins, int userId)
    {
        return await logins.AsNoTracking()
            .Where(login => login.UserId == userId && !login.IsDeleted)
            .AnyAsync();
    }
}