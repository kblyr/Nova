using System.Security.Cryptography;

namespace Nova.Security;

sealed class InlineXmlStringEncryptionKeyProvider : IStringEncryptionKeyProvider
{
    readonly string _keyXmlString;

    public InlineXmlStringEncryptionKeyProvider(string keyXmlString)
    {
        _keyXmlString = keyXmlString;
    }

    public void Load(RSA algorithm)
    {
        algorithm.FromXmlString(_keyXmlString);
    }
}

sealed class InlineXmlStringDecryptionKeyProvider : IStringDecryptionKeyProvider
{
    readonly string _keyXmlString;

    public InlineXmlStringDecryptionKeyProvider(string keyXmlString)
    {
        _keyXmlString = keyXmlString;
    }

    public void Load(RSA algorithm)
    {
        algorithm.FromXmlString(_keyXmlString);
    }
}

sealed class XmlFileStringEncryptionKeyProvider : IStringEncryptionKeyProvider
{
    readonly string _filePath;

    public XmlFileStringEncryptionKeyProvider(string filePath)
    {
        _filePath = filePath;
    }

    public void Load(RSA algorithm)
    {
        if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
            return;

        algorithm.FromXmlString(File.ReadAllText(_filePath));
    }
}

sealed class XmlFileStringDecryptionKeyProvider : IStringDecryptionKeyProvider
{
    readonly string _filePath;

    public XmlFileStringDecryptionKeyProvider(string filePath)
    {
        _filePath = filePath;
    }

    public void Load(RSA algorithm)
    {
        if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
            return;

        algorithm.FromXmlString(File.ReadAllText(_filePath));
    }
}