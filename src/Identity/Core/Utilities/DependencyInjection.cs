using Microsoft.Extensions.DependencyInjection;
using Nova.Identity.Utilities;

namespace Nova.Identity.Core.Utilities;

public static class DependencyExtensions
{
    public static DependencyInjector AddUtilities(this DependencyInjector injector)
    {
        injector.Services
            .AddSingleton<IUserPasswordHash, UserPasswordHash>();
        return injector;
    }
}