using System.Security.Cryptography;

namespace Nova.Security;

sealed class InlinePemStringEncryptionKeyProvider : IStringEncryptionKeyProvider
{
    readonly string _keyPemString;

    public InlinePemStringEncryptionKeyProvider(string keyPemString)
    {
        _keyPemString = keyPemString;
    }

    public void Load(RSA algorithm)
    {
        algorithm.ImportFromPem(_keyPemString);
    }
}

sealed class InlinePemStringDecryptionKeyProvider : IStringDecryptionKeyProvider
{
    readonly string _keyPemString;

    public InlinePemStringDecryptionKeyProvider(string keyPemString)
    {
        _keyPemString = keyPemString;
    }

    public void Load(RSA algorithm)
    {
        algorithm.ImportFromPem(_keyPemString);
    }
}

sealed class PemFileStringEncryptionKeyProvider : IStringEncryptionKeyProvider
{
    readonly string _filePath;

    public PemFileStringEncryptionKeyProvider(string filePath)
    {
        _filePath = filePath;
    }

    public void Load(RSA algorithm)
    {
        if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
            return;

        algorithm.ImportFromPem(File.ReadAllText(_filePath));
    }
}

sealed class PemFileStringDecryptionKeyProvider : IStringDecryptionKeyProvider
{
    readonly string _filePath;

    public PemFileStringDecryptionKeyProvider(string filePath)
    {
        _filePath = filePath;
    }

    public void Load(RSA algorithm)
    {
        if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
            return;

        algorithm.ImportFromPem(File.ReadAllText(_filePath));
    }
}
