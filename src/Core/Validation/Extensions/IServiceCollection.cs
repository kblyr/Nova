using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Validation;

public static class IServiceCollection_Extensions
{
    public static IServiceCollection AddAccessValidationConfigurations(this IServiceCollection services, params Assembly[] assemblies)
    {
        var openGenericType = typeof(IAccessValidationConfiguration<>);

        foreach (var assembly in assemblies)
        {
            var configurationTypes = assembly.GetTypes()
                .Where(type =>
                    !type.IsAbstract
                    && !type.IsGenericTypeDefinition
                )
                .Select(type => new
                {
                    ConcreteType = type,
                    InterfaceType = type.GetInterfaces().FirstOrDefault(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGenericType)
                })
                .Where(x => x.InterfaceType is not null);

            if (configurationTypes?.Any() ?? false)
            {
                foreach (var configurationType in configurationTypes)
                {
                    if (configurationType.InterfaceType is null || configurationType.ConcreteType is null)
                        continue;

                    services.AddScoped(configurationType.InterfaceType, configurationType.ConcreteType);
                }
            }
        }

        return services;
    }

    public static IServiceCollection AddAccessValidators(this IServiceCollection services, params Assembly[] assemblies)
    {
        var openGenericType = typeof(IAccessValidator<>);

        foreach (var assembly in assemblies)
        {
            var configurationTypes = assembly.GetTypes()
                .Where(type =>
                    !type.IsAbstract
                    && !type.IsGenericTypeDefinition
                )
                .Select(type => new
                {
                    ConcreteType = type,
                    InterfaceType = type.GetInterfaces().FirstOrDefault(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == openGenericType)
                })
                .Where(x => x.InterfaceType is not null);

            if (configurationTypes?.Any() ?? false)
            {
                foreach (var configurationType in configurationTypes)
                {
                    if (configurationType.InterfaceType is null || configurationType.ConcreteType is null)
                        continue;

                    services.AddScoped(configurationType.InterfaceType, configurationType.ConcreteType);
                }
            }
        }

        return services;
    }
}