using System.Reflection;

namespace Nova;

public interface IApiResponseTypeRegistry
{
    Type? this[string key] { get; }
    bool TryGet(string key, out Type responseType);
    IApiResponseTypeRegistry Register(Type responseType);
}

sealed class ApiResponseTypeRegistry : IApiResponseTypeRegistry
{
    static readonly Type _apiResponseInterfaceType = typeof(IApiResponse);

    readonly Dictionary<string, Type> _responseTypes = new();
    readonly IApiResponseTypeRegistryKeyProvider _keyProvider;
    readonly AssemblyScanner _assemblyScanner;

    public ApiResponseTypeRegistry(IApiResponseTypeRegistryKeyProvider keyProvider, AssemblyScanner assemblyScanner)
    {
        _keyProvider = keyProvider;
        _assemblyScanner = assemblyScanner;
    }

    public Type? this[string key]
    {
        get 
        {
            if (_responseTypes.ContainsKey(key))
                return _responseTypes[key];

            if (!_assemblyScanner.IsScanned)
            {
                _assemblyScanner.Scan(this);
                return this[key];
            }

            return null;
        }
    }

    public bool TryGet(string key, out Type responseType)
    {
        if (_responseTypes.ContainsKey(key))
        {
            responseType = _responseTypes[key];
            return true;
        }

        if (!_assemblyScanner.IsScanned)
        {
            _assemblyScanner.Scan(this);
            return TryGet(key, out responseType);
        }

        responseType = default!;
        return false;
    }

    public IApiResponseTypeRegistry Register(Type responseType)
    {
        var key = _keyProvider.Get(responseType);

        if (!_responseTypes.ContainsKey(key))
            _responseTypes.Add(key, responseType);

        return this;
    }

    public record AssemblyScanner(IEnumerable<Assembly> Assemblies)
    {
        public bool IsScanned { get; private set; }

        public void Scan(ApiResponseTypeRegistry registry)
        {
            IsScanned = true;

            if (Assemblies is null || !Assemblies.Any())
                return;

            var tMarker = typeof(IApiResponse);

            foreach (var assembly in Assemblies)
            {
                var tApiResponses = assembly.GetTypes().Where(t =>
                    !t.IsAbstract
                    && !t.IsGenericType
                    && !t.GetInterfaces().Any(tInterface => tInterface == tMarker) 
                );

                if (tApiResponses is null || !tApiResponses.Any())
                    continue;

                foreach (var tApiResponse in tApiResponses)
                    registry.Register(tApiResponse);
            }
        }
    }
}