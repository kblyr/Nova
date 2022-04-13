using System.Reflection;

namespace Nova;

public interface IResponseTypeMapRegistry
{
    ResponseTypeMapDefinition? this[Type responseType] { get; }
    bool TryGet(Type responseType, out ResponseTypeMapDefinition definition);
    IResponseTypeMapRegistry Register(ResponseTypeMapDefinition definition);
    IResponseTypeMapRegistry Register(Type responseType, Type apiResponseType, int statusCode);
}

sealed class ResponseTypeMapRegistry : IResponseTypeMapRegistry
{
    readonly Dictionary<Type, ResponseTypeMapDefinition> _definitions = new();
    readonly AssemblyScanner _assemblyScanner;

    public ResponseTypeMapRegistry(AssemblyScanner assemblyScanner)
    {
        _assemblyScanner = assemblyScanner;
    }

    public ResponseTypeMapDefinition? this[Type responseType]
    {
        get
        {
            if (_definitions.ContainsKey(responseType))
                return _definitions[responseType];

            if (!_assemblyScanner.IsScanned)
            {
                _assemblyScanner.Scan(this);
                return this[responseType];
            }

            return null;
        }
    }

    public bool TryGet(Type responseType, out ResponseTypeMapDefinition definition)
    {
        if (_definitions.ContainsKey(responseType))
        {
            definition = _definitions[responseType];
            return true;
        }

        if (!_assemblyScanner.IsScanned)
        {
            _assemblyScanner.Scan(this);
            return TryGet(responseType, out definition);
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

    public record AssemblyScanner(IEnumerable<Assembly> Assemblies)
    {
        public bool IsScanned { get; private set; }

        public void Scan(ResponseTypeMapRegistry registry)
        {
            IsScanned = true;

            if (Assemblies is null || !Assemblies.Any())
                return;

            var tMarker = typeof(IResponseTypeMapRegistration);

            foreach (var assembly in Assemblies)
            {
                var tRegistrations = assembly.GetTypes().Where(t =>
                    !t.IsAbstract
                    && !t.IsGenericType
                    && t.GetInterfaces().Any(tInterface => tInterface == tMarker)
                    && t.GetConstructor(Type.EmptyTypes) is not null
                );

                if (tRegistrations is null || !tRegistrations.Any())
                    continue;

                foreach (var tRegistration in tRegistrations)
                {
                    var registration = Activator.CreateInstance(tRegistration) as IResponseTypeMapRegistration;
                    registration?.Register(registry);
                }
            }
        }
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