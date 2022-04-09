using System.Reflection;

namespace Nova;

public interface IResponseTypeMapRegistry
{
    ResponseTypeMapDefinition? this[Type responseType] { get; }
    bool TryGet(Type responseType, out ResponseTypeMapDefinition definition);
    IResponseTypeMapRegistry Register(ResponseTypeMapDefinition definition);
    IResponseTypeMapRegistry Register(Type responseType, Type apiResponseType, int statusCode);
    IResponseTypeMapRegistry Register(IEnumerable<Assembly> assemblies);
}

sealed class ResponseTypeMapRegistry : IResponseTypeMapRegistry
{
    static readonly Type _responseTypeMapRegistrationType = typeof(IResponseTypeMapRegistration);
    static readonly Dictionary<Type, ResponseTypeMapDefinition> _definitions = new();

    public ResponseTypeMapDefinition? this[Type responseType] => throw new NotImplementedException();

    public bool TryGet(Type responseType, out ResponseTypeMapDefinition definition)
    {
        if (_definitions.ContainsKey(responseType))
        {
            definition = _definitions[responseType];
            return true;
        }
        
        definition = default;
        return false;
    }

    public IResponseTypeMapRegistry Register(ResponseTypeMapDefinition definition)
    {
        if (!_definitions.ContainsKey(definition.ResponseType))
            _definitions.Add(definition.ResponseType, definition);
        return this;
    }

    public IResponseTypeMapRegistry Register(Type responseType, Type apiResponseType, int statusCode)
    {
        if (!_definitions.ContainsKey(responseType))
            _definitions.Add(responseType, new(responseType, apiResponseType, statusCode));

        return this;
    }

    public IResponseTypeMapRegistry Register(IEnumerable<Assembly> assemblies)
    {
        if (assemblies.Any())
        {
            foreach (var assembly in assemblies)
            {
                var registryTypes = assembly.GetTypes()
                    .Where(type =>
                        !type.IsGenericType
                        && !type.IsAbstract
                        && type.GetInterfaces().Any(interfaceType => interfaceType == _responseTypeMapRegistrationType)
                    );

                if (registryTypes is null || !registryTypes.Any())
                    continue;

                foreach (var registrationType in registryTypes)
                {
                    var registration = Activator.CreateInstance(registrationType) as IResponseTypeMapRegistration;
                    registration?.Register(this);
                }
            }
        }

        return this;
    }
}

public record struct ResponseTypeMapDefinition
(
    Type ResponseType,
    Type ApiResponseType,
    int StatusCode
);

public interface IResponseTypeMapRegistration
{
    void Register(IResponseTypeMapRegistry registry);
}