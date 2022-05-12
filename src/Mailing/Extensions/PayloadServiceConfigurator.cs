using Microsoft.Extensions.DependencyInjection;

namespace Nova;

public static class PayloadServiceConfiguratorExtensions
{
    public static PayloadServiceConfigurator<T> WithMailTemplateFromFile<T>(this PayloadServiceConfigurator<T> configurator, string filePath, MailTemplateLoaderOptions<T>? options = null)
    {
        configurator.Services.AddSingleton<IMailTemplateLoader<T>>(provider => new MailTemplateLoaderFromFile<T>(provider.GetRequiredService<MailTemplateCache>(), filePath, options));
        return configurator;
    }

    public static PayloadServiceConfigurator<T> WithMailComposer<T, TComposer>(this PayloadServiceConfigurator<T> configurator) where TComposer : class, IMailComposer<T>
    {
        configurator.Services.AddSingleton<IMailComposer<T>, TComposer>();
        return configurator;
    }
}