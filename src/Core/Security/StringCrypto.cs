using System.Security.Cryptography;
using System.Text;

namespace Nova.Security;

public interface IStringEncryptor : IDisposable
{
    string Encrypt(string text);
}

public interface IStringDecryptor : IDisposable
{
    string Decrypt(string cipher);
}

abstract class StringCryptoProcessBase<TKeyProvider> where TKeyProvider : IStringKeyProvider
{
    readonly TKeyProvider _keyProvider;

    protected StringCryptoProcessBase(TKeyProvider keyProvider)
    {
        _keyProvider = keyProvider;
    }

    RSA? _algorithm;
    protected RSA Algorithm => _algorithm ??= InitializeAlgorithm();

    private RSA InitializeAlgorithm()
    {
        var algorithm = RSA.Create();
        _keyProvider.Load(algorithm);
        return algorithm;
    }

    public virtual void Dispose()
    {
        _algorithm?.Dispose();
    }
}

sealed class StringEncryptor : StringCryptoProcessBase<IStringEncryptionKeyProvider>, IStringEncryptor
{
    public StringEncryptor(IStringEncryptionKeyProvider keyProvider) : base(keyProvider)
    {
    }

    public string Encrypt(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "";

        var data = Encoding.UTF8.GetBytes(text);
        var cipherData = Algorithm.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
        var cipherText = Convert.ToBase64String(cipherData);
        return cipherText;
    }
}

sealed class StringDecryptor : StringCryptoProcessBase<IStringDecryptionKeyProvider>, IStringDecryptor
{
    public StringDecryptor(IStringDecryptionKeyProvider keyProvider) : base(keyProvider)
    {
    }

    public string Decrypt(string cipher)
    {
        if (string.IsNullOrWhiteSpace(cipher))
            return "";

        var cipherData = Convert.FromBase64String(cipher);
        var data = Algorithm.Decrypt(cipherData, RSAEncryptionPadding.OaepSHA256);
        var text = Encoding.UTF8.GetString(data);
        return text;
    }
}