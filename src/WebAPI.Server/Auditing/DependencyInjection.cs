using Microsoft.Extensions.DependencyInjection;
using Nova.Auditing;

namespace Nova.WebAPI.Server.Auditing;

public static class DependencyExtensions
{
    public static Server.DependencyInjector AddAuditing(this Server.DependencyInjector injector)
    {
        injector.Services.AddSingleton<ICurrentAuditInfoProvider, CurrentAuditInfoProvider>();
        return injector;
    }
}
