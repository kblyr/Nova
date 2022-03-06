using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nova.Messaging;

public static class IServiceCollection_Extensions
{
    public static IServiceCollection AddResponseMapping(this IServiceCollection services, params Assembly[] assemblies)
    {
        var registry = new ResponseTypeRegistry();

        foreach (var assembly in assemblies)
        {
            var responseTypeRegistrationTypes = assembly.GetTypes().Where(type =>
                type.IsClass
                && !type.IsAbstract
                && !type.IsGenericType
                && type.GetInterfaces().Any(interfaceType => interfaceType.Equals(typeof(IResponseTypeRegistration)))
            );

            if (responseTypeRegistrationTypes is null)
                continue;

            foreach (var responseTypeRegistrationType in responseTypeRegistrationTypes)
            {
                var instance = (IResponseTypeRegistration?)Activator.CreateInstance(responseTypeRegistrationType);
                instance?.Register(registry);
            }
        }

        return services.AddSingleton(registry);
    }
}