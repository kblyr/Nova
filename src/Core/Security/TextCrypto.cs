using System.Security.Cryptography;
using System.Text;

namespace Nova.Security;

public interface ITextEncryptor : IDisposable
{
    string Encrypt(string text);
}

public interface ITextDecryptor : IDisposable
{
    string Decrypt(string cipherText);
}

public abstract class TextCryptorBase<TKeyLoader> where TKeyLoader : ITextCryptoKeyLoader
{
    readonly TKeyLoader _keyLoader;

    protected TextCryptorBase(TKeyLoader keyLoader)
    {
        _keyLoader = keyLoader;
    }

    RSA? _algorithm;
    protected RSA Algorithm => _algorithm ??= InitializeAlgorithm();

    RSA InitializeAlgorithm()
    {
        var algorithm = RSA.Create();
        _keyLoader.Load(algorithm);
        return algorithm;
    }

    public virtual void Dispose()
    {
        _algorithm?.Dispose();
    }
}

public abstract class TextEncryptorBase : TextCryptorBase<ITextEncryptionKeyLoader>
{
    protected TextEncryptorBase(ITextEncryptionKeyLoader keyLoader) : base(keyLoader)
    {
    }

    public string Encrypt(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return "";
        }

        var data = Encoding.UTF8.GetBytes(text);
        var cipherData = Algorithm.Encrypt(data, RSAEncryptionPadding.OaepSHA512);
        var cipherText = Convert.ToBase64String(cipherData);
        return cipherText;
    }
}

public abstract class TextDecryptorBase : TextCryptorBase<ITextDecryptionKeyLoader>
{
    protected TextDecryptorBase(ITextDecryptionKeyLoader keyLoader) : base(keyLoader)
    {
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrWhiteSpace(cipherText))
        {
            return "";
        }

        var cipherData = Convert.FromBase64String(cipherText);
        var data = Algorithm.Decrypt(cipherData, RSAEncryptionPadding.OaepSHA512);
        var text = Encoding.UTF8.GetString(data);
        return text;
    }
}
