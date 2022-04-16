using System.Reflection;

namespace Nova.Validation;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAccessValidationConfigurations(this IServiceCollection services, params Assembly[] assemblies)
    {
        var tMarker = typeof(IAccessValidationConfiguration<>);

        foreach (var assembly in assemblies)
        {
            var tConfigurations = assembly.GetTypes()
                .Where(t =>
                    !t.IsAbstract
                    && !t.IsGenericType
                )
                .Select(t => new
                {
                    ConcreteType = t,
                    InterfaceType = t.GetInterfaces().FirstOrDefault(tInterface => tInterface.IsGenericType && tInterface.GetGenericTypeDefinition() == tMarker)
                })
                .Where(x => x.InterfaceType is not null);

            if (tConfigurations is null || !tConfigurations.Any())
                continue;

            foreach (var tConfiguration in tConfigurations)
            {
                if (tConfiguration.InterfaceType is null || tConfiguration.ConcreteType is null)
                    continue;

                services.AddScoped(tConfiguration.InterfaceType, tConfiguration.ConcreteType);
            }
        }

        return services;
    }

    public static IServiceCollection AddAccessValidators(this IServiceCollection services, params Assembly[] assemblies)
    {
        var tMarker = typeof(IAccessValidator<>);

        foreach (var assembly in assemblies)
        {
            var tConfigurations = assembly.GetTypes()
                .Where(t =>
                    !t.IsAbstract
                    && !t.IsGenericTypeDefinition
                )
                .Select(t => new
                {
                    ConcreteType = t,
                    InterfaceType = t.GetInterfaces().FirstOrDefault(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == tMarker)
                })
                .Where(x => x.InterfaceType is not null);

            if (tConfigurations is null || !tConfigurations.Any())
                continue;

            foreach (var tConfiguration in tConfigurations)
            {
                if (tConfiguration.InterfaceType is null || tConfiguration.ConcreteType is null)
                    continue;

                services.AddScoped(tConfiguration.InterfaceType, tConfiguration.ConcreteType);
            }
        }

        return services;
    }
}