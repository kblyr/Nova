using System.Security.Cryptography;
using System.Text;

namespace Nova.Identity.Utilities;

sealed class UserPasswordHash : IUserPasswordHash
{
    public string Compute(string password)
    {
        if (password is null)
            return "";

        using var algo = SHA512.Create();
        var passwordData = Encoding.UTF8.GetBytes(password);
        var cipherData = algo.ComputeHash(passwordData);
        return new StringBuilder(BitConverter.ToString(cipherData))
            .Replace("-", "")
            .ToString();
    }
}