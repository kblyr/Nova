using Microsoft.Extensions.DependencyInjection;
using Nova.Authentication;

namespace Nova.Web.Authentication;

public static class DependencyExtensions
{
    public static DependencyInjector AddAuthentication(this DependencyInjector injector)
    {
        injector.Services.AddScoped<ICurrentSessionProvider, CurrentSessionProvider>();
        return injector;
    }
}