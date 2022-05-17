using Nova.Exceptions;

namespace Nova;

public interface IApiResponseTypeRegistryKeyProvider
{
    string Get(Type responseType);
}

sealed class ApiResponseTypeRegistryKeyProvider : IApiResponseTypeRegistryKeyProvider
{
    static readonly Type _tSchemaIdAttr = typeof(SchemaIdAttribute);

    readonly Dictionary<Type, string> _registryKeys = new();

    public string Get(Type responseType)
    {
        if (_registryKeys.ContainsKey(responseType))
            return _registryKeys[responseType];

        string? key = null;
        var schemaIdAttr = responseType.GetCustomAttributes(_tSchemaIdAttr, false).FirstOrDefault() as SchemaIdAttribute;
        key = schemaIdAttr?.SchemaId ?? responseType.FullName;
        _registryKeys.Add(responseType, key ?? throw new FailedToGetApiResponseTypeRegistryKeyException(responseType));
        return key;
    }
}