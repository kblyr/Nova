using Microsoft.Extensions.DependencyInjection;

namespace Nova.Web.Messaging;

public static class DependencyExtensions
{
    public static DependencyInjector AddMessaging(this DependencyInjector injector)
    {
        injector.Services
            .AddSingleton<MappedMediator>()
            .AddSingleton<ResponseMapper>()
            .AddResponseMapping(injector.AssemblyMarkers);

        return injector;
    }
}