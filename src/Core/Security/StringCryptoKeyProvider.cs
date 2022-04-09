using System.Security.Cryptography;

namespace Nova.Security;

public interface IStringKeyProvider
{
    void Load(RSA algorithm);
}

public interface IStringEncryptionKeyProvider : IStringKeyProvider { }

public interface IStringDecryptionKeyProvider : IStringKeyProvider { }