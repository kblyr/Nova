namespace Nova.Identity;

public static partial class DbSetExtensions
{
    public static async Task<bool> Exists(this DbSet<UserEmailAddress> userEmailAddresses, string emailAddress)
    {
        return await userEmailAddresses.AsNoTracking()
            .Where(userEmailAddress => userEmailAddress.EmailAddress == emailAddress && !userEmailAddress.IsDeleted)
            .AnyAsync();
    }
}