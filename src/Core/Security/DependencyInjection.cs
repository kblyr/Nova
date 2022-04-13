using Nova.Security;

namespace Nova.Core.Security;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IDependencyInjector parent) : base(parent)
    {
    }
}

public static class DependencyExtensions
{
    public static Nova.Core.DependencyInjector WithSecurity(this Nova.Core.DependencyInjector injector, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var _injector = new DependencyInjector(injector);
        injectDependencies?.Invoke(_injector);
        return injector;
    }

    public static DependencyInjector AddStringEncryptor(this DependencyInjector injector, IStringEncryptionKeyProvider keyProvider)
    {
        injector.Services
            .AddSingleton<IStringEncryptor, StringEncryptor>()
            .AddSingleton<IStringEncryptionKeyProvider>(keyProvider);
        return injector;
    }

    public static DependencyInjector AddStringDecryptor(this DependencyInjector injector, IStringDecryptionKeyProvider keyProvider)
    {
        injector.Services
            .AddSingleton<IStringDecryptor, StringDecryptor>()
            .AddSingleton<IStringDecryptionKeyProvider>(keyProvider);
        return injector;
    }

    public static DependencyInjector AddStringEncryptorWithInlineXmlKey(this DependencyInjector injector, string keyXmlString)
    {
        return injector.AddStringEncryptor(new InlineXmlStringEncryptionKeyProvider(keyXmlString));
    }

    public static DependencyInjector AddStringDecryptorWithInlineXmlKey(this DependencyInjector injector, string keyXmlString)
    {
        return injector.AddStringDecryptor(new InlineXmlStringDecryptionKeyProvider(keyXmlString));
    }

    public static DependencyInjector AddStringEncryptorWithXmlKeyFile(this DependencyInjector injector, string filePath)
    {
        return injector.AddStringEncryptor(new XmlFileStringEncryptionKeyProvider(filePath));
    }

    public static DependencyInjector AddStringDecryptorWithXmlKeyFile(this DependencyInjector injector, string filePath)
    {
        return injector.AddStringDecryptor(new XmlFileStringDecryptionKeyProvider(filePath));
    }

    public static DependencyInjector AddStringEncryptorWithInlinePemKey(this DependencyInjector injector, string keyPemString)
    {
        return injector.AddStringEncryptor(new InlinePemStringEncryptionKeyProvider(keyPemString));
    }

    public static DependencyInjector AddStringDecryptorWithInlinePemKey(this DependencyInjector injector, string keyPemString)
    {
        return injector.AddStringDecryptor(new InlinePemStringDecryptionKeyProvider(keyPemString));
    }

    public static DependencyInjector AddStringEncryptorWithPemFile(this DependencyInjector injector, string filePath)
    {
        return injector.AddStringEncryptor(new PemFileStringEncryptionKeyProvider(filePath));
    }

    public static DependencyInjector AddStringDecryptorWithPemFile(this DependencyInjector injector, string filePath)
    {
        return injector.AddStringDecryptor(new PemFileStringDecryptionKeyProvider(filePath));
    }
}
