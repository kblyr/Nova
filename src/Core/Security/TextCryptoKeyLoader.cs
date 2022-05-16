using System.Security.Cryptography;

namespace Nova.Security;

public interface ITextCryptoKeyLoader
{
    void Load(RSA algo);
}

public interface ITextEncryptionKeyLoader : ITextCryptoKeyLoader { }

public interface ITextDecryptionKeyLoader : ITextCryptoKeyLoader { }

public abstract class PemFileTextCryptoKeyLoaderBase
{
    readonly string _filePath;

    protected PemFileTextCryptoKeyLoaderBase(string filePath)
    {
        _filePath = filePath;
    }

    public void Load(RSA algo)
    {
        if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
        {
            return;
        }

        algo.ImportFromPem(File.ReadAllText(_filePath));
    }
}