using System.Runtime.Serialization;

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

sealed class FailedToGetApiResponseTypeRegistryKeyException : Exception
{
    public Type ResponseType { get; }

    public FailedToGetApiResponseTypeRegistryKeyException(Type responseType)
    {
        ResponseType = responseType;
    }

    public FailedToGetApiResponseTypeRegistryKeyException(Type responseType, string? message) : base(message)
    {
        ResponseType = responseType;
    }

    public FailedToGetApiResponseTypeRegistryKeyException(Type responseType, SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ResponseType = responseType;
    }

    public FailedToGetApiResponseTypeRegistryKeyException(Type responseType, string? message, Exception? innerException) : base(message, innerException)
    {
        ResponseType = responseType;
    }
}