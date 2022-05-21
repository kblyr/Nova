using System.Security.Cryptography;

namespace Nova.Identity.Security;

public interface IUserPasswordHashAlgorithm
{
    UserPasswordHashAlgorithmComputeResult Compute(string password);
    UserPasswordHashAlgorithmComputeResult ComputeF2BCipher(string cipherPassword);
    string Compute(string password, string salt);
    string ComputeF2BCipher(string cipherPassword, string salt);
    bool Verify(string hashedPassword, string password, string salt);
    bool VerifyF2BCipher(string hashedPassword, string cipherPassword, string salt);
}

public record struct UserPasswordHashAlgorithmComputeResult(string HashedPassword, string Salt);

sealed class UserPasswordHashAlgorithm : IUserPasswordHashAlgorithm
{
    const int SaltLength = 32;
    const int Iterations = 1_000;
    const int HashLength = 128;

    readonly IServiceProvider _services;

    public UserPasswordHashAlgorithm(IServiceProvider services)
    {
        _services = services;
    }

    string ComputeInternal(string password, byte[] salt)
    {
        using var algorithm = new Rfc2898DeriveBytes(password, salt, Iterations);
        return ConvertDataToText(algorithm.GetBytes(HashLength));
    }

    public UserPasswordHashAlgorithmComputeResult Compute(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(password));
        }

        var salt = GenerateSalt();
        return new()
        {
            HashedPassword = ComputeInternal(password, salt),
            Salt = ConvertDataToText(salt)
        };
    }

    public string Compute(string password, string salt)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(password));
        }

        if (string.IsNullOrWhiteSpace(salt))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(salt));
        }

        return ComputeInternal(password, ConvertTextToData(salt));
    }

    public UserPasswordHashAlgorithmComputeResult ComputeF2BCipher(string cipherPassword)
    {
        if (string.IsNullOrWhiteSpace(cipherPassword))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(cipherPassword));
        }
        
        var decryptor = _services.GetRequiredService<IUserPasswordF2BDecryptor>();
        return Compute(decryptor.Decrypt(cipherPassword));
    }

    public string ComputeF2BCipher(string cipherPassword, string salt)
    {
        if (string.IsNullOrWhiteSpace(cipherPassword))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(cipherPassword));
        }
        
        var decryptor = _services.GetRequiredService<IUserPasswordF2BDecryptor>();
        return Compute(decryptor.Decrypt(cipherPassword), salt);
    }

    public bool Verify(string hashedPassword, string password, string salt)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(hashedPassword));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(password));
        }

        if (string.IsNullOrWhiteSpace(salt))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(salt));
        }

        return string.Equals(hashedPassword, ComputeInternal(password, ConvertTextToData(salt)));
    }

    public bool VerifyF2BCipher(string hashedPassword, string cipherPassword, string salt)
    {
        if (string.IsNullOrWhiteSpace(cipherPassword))
        {
            throw new ArgumentException("Cannot be null or white-space", nameof(cipherPassword));
        }
        
        var decryptor = _services.GetRequiredService<IUserPasswordF2BDecryptor>();
        return Verify(hashedPassword, decryptor.Decrypt(cipherPassword), salt);
    }

    static byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(SaltLength);
    }

    static byte[] ConvertTextToData(string text)
    {
        return Convert.FromBase64String(text);
    }

    static string ConvertDataToText(byte[] data)
    {
        return Convert.ToBase64String(data);
    }
}
