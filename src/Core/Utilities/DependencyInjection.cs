using Microsoft.Extensions.DependencyInjection;
using Nova.Utilities;

namespace Nova.Core.Utilities;

public static class DependencyExtensions
{
    public static DependencyInjector AddUtilities(this DependencyInjector injector)
    {
        injector.Services
            .AddSingleton<RandomStringGenerator>();
        return injector;
    }
}