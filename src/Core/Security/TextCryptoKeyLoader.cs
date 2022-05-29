using System.Security.Cryptography;
using Nova.Security.Exceptions;

namespace Nova.Security;

public interface ITextCryptoKeyLoader
{
    void Load(RSA algorithm);
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

    public void Load(RSA algorithm)
    {
        if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
        {
            throw new CryptoKeyFileNotFoundException("Crypto key file does not exists", _filePath);
        }

        algorithm.ImportFromPem(File.ReadAllText(_filePath));
    }
}