using System.Reflection;
using Microsoft.AspNetCore.Routing;

namespace Nova.Web;

public static class IEndpointRouteBuilder_Extensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var endpointMapperTypes = assembly.GetTypes().Where(type =>
                type.IsClass
                && !type.IsAbstract
                && !type.IsGenericType
                && type.GetInterfaces().Any(interfaceType => interfaceType.Equals(typeof(EndpointMapper)))
            );

            if (endpointMapperTypes is null)
                continue;

            foreach (var endpointMapperType in endpointMapperTypes)
            {
                var instance = (EndpointMapper?)Activator.CreateInstance(endpointMapperType);
                instance?.Map(builder);
            }
        }
        
        return builder;
    }
}