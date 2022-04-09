using System.Reflection;

namespace Nova;

public interface IApiResponseTypeRegistry
{
    Type? this[string key] { get; }
    bool TryGet(string key, out Type responseType);
    IApiResponseTypeRegistry Register(Type responseType);
    IApiResponseTypeRegistry Register(IEnumerable<Assembly> assemblies);
}

sealed class ApiResponseTypeRegistry : IApiResponseTypeRegistry
{
    static readonly Type _apiResponseInterfaceType = typeof(IApiResponse);

    readonly Dictionary<string, Type> _responseTypes = new();
    readonly IApiResponseTypeRegistryKeyProvider _keyProvider;

    public ApiResponseTypeRegistry(IApiResponseTypeRegistryKeyProvider keyProvider)
    {
        _keyProvider = keyProvider;
    }

    public Type? this[string key]
    {
        get 
        {
            if (_responseTypes.ContainsKey(key))
                return _responseTypes[key];

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

    public IApiResponseTypeRegistry Register(IEnumerable<Assembly> assemblies)
    {
        if (assemblies.Any())
        {
            foreach (var assembly in assemblies)
            {
                var responseTypes = assembly.GetTypes()
                    .Where(type =>
                        !type.IsGenericType
                        && !type.IsAbstract
                        && type.GetInterfaces().Any(interfaceType => interfaceType == _apiResponseInterfaceType)
                    );

                if (responseTypes is null || !responseTypes.Any())
                    continue;

                foreach (var responseType in responseTypes)
                    Register(responseType);
            }
        }

        return this;
    }
}