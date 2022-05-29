namespace Nova.Mailing;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IServiceCollection services) : base(services)
    {
    }
}

public static class DependencyExtensions
{
    public static IServiceCollection AddNovaMailing(this IServiceCollection services, InjectDependencies<DependencyInjector> injectDependencies)
    {
        var injector = new DependencyInjector(services);
        injectDependencies(injector);
        return services;
    }

    public static DependencyInjector AddSender<T>(this DependencyInjector injector) where T : class, IMailSender
    {
        injector.Services.AddSingleton<T>();
        return injector;
    }

    public static DependencyInjector AddTemplateLoaderFromFile<T>(this DependencyInjector injector) where T : MailTemplateLoaderFromFile
    {
        injector.Services.AddSingleton<T>();
        return injector;
    }
}