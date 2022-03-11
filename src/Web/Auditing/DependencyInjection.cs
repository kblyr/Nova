using Microsoft.Extensions.DependencyInjection;
using Nova.Auditing;

namespace Nova.Web.Auditing;

public static class DependencyExtensions
{
    public static DependencyInjector AddAuditing(this DependencyInjector injector)
    {
        injector.Services
            .AddScoped<ICurrentAuditInfoProvider, CurrentAuditInfoProvider>();
        return injector;
    }
}