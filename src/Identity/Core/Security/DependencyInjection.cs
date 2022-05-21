using Nova.Identity.Security;

namespace Nova.Identity.Core.Security;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services) : base(services)
    {
    }
}

public static class DependencyExtensions
{
    public static Core.DependencyInjector AddSecurity(this Core.DependencyInjector injector, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var _injector = new DependencyInjector(injector.Services);
        injector.Services
            .TryAddSingleton<IUserPasswordHashAlgorithm, UserPasswordHashAlgorithm>();
        injectDependencies(_injector);
        return injector;
    }

    public static Core.DependencyInjector AddSecurity(this Core.DependencyInjector injector)
    {
        return injector.AddSecurity(injector => {});
    }

    public static DependencyInjector AddUserAutoGeneratedPasswordEncryptorFromPemFile(this DependencyInjector injector, string pemKeyFilePath)
    {
        injector.Services
            .AddSingleton<UserAutoGeneratedPasswordEncryptor>()
            .AddSingleton<PemFileUserAutoGeneratedPasswordEncryptionKeyLoader>(sp => new (pemKeyFilePath));
        return injector;
    }

    public static DependencyInjector AddUserAutoGeneratedPasswordDecryptorFromPemFile(this DependencyInjector injector, string pemKeyFilePath)
    {
        injector.Services
            .AddSingleton<UserAutoGeneratedPasswordDecryptor>()
            .AddSingleton<PemFileUserAutoGeneratedPasswordDecryptionKeyLoader>(sp => new(pemKeyFilePath));
        return injector;
    }

    public static DependencyInjector AddUserPasswordF2BEncryptionWithKeyFromFile(this DependencyInjector injector, string filePath)
    {
        injector.Services
            .AddSingleton<IUserPasswordF2BEncryptor, UserPasswordF2BEncryptor>()
            .AddSingleton<IUserPasswordF2BEncryptionKeyLoader>(sp => new PemFileUserPasswordF2BEncryptionKeyLoader(filePath));
        return injector;
    }

    public static DependencyInjector AddUserPasswordF2BDecryptionWithKeyFromFile(this DependencyInjector injector, string filePath)
    {
        injector.Services
            .AddSingleton<IUserPasswordF2BDecryptor, UserPasswordF2BDecryptor>()
            .AddSingleton<IUserPasswordF2BDecryptionKeyLoader>(sp => new PemFileUserPasswordF2BDecryptionKeyLoader(filePath));
        return injector;
    }
}
