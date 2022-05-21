namespace Nova.Identity.Security;

public interface IUserPasswordF2BEncryptor : ITextEncryptor { }

public interface IUserPasswordF2BDecryptor : ITextDecryptor { }

public interface IUserPasswordF2BEncryptionKeyLoader : ITextEncryptionKeyLoader { }

public interface IUserPasswordF2BDecryptionKeyLoader : ITextDecryptionKeyLoader { }

sealed class UserPasswordF2BEncryptor : TextEncryptorBase, IUserPasswordF2BEncryptor
{
    public UserPasswordF2BEncryptor(IUserPasswordF2BEncryptionKeyLoader keyLoader) : base(keyLoader)
    {
    }
}

sealed class UserPasswordF2BDecryptor : TextDecryptorBase, IUserPasswordF2BDecryptor
{
    public UserPasswordF2BDecryptor(IUserPasswordF2BDecryptionKeyLoader keyLoader) : base(keyLoader)
    {
    }
}

sealed class PemFileUserPasswordF2BEncryptionKeyLoader : PemFileTextCryptoKeyLoaderBase, IUserPasswordF2BEncryptionKeyLoader
{
    public PemFileUserPasswordF2BEncryptionKeyLoader(string filePath) : base(filePath)
    {
    }
}

sealed class PemFileUserPasswordF2BDecryptionKeyLoader : PemFileTextCryptoKeyLoaderBase, IUserPasswordF2BDecryptionKeyLoader
{
    public PemFileUserPasswordF2BDecryptionKeyLoader(string filePath) : base(filePath)
    {
    }
}
