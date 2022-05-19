using Microsoft.Extensions.DependencyInjection;
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
        injectDependencies(_injector);
        return injector;
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
}
