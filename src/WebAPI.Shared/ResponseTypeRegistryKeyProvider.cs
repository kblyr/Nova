using System.Runtime.Serialization;

namespace Nova;

public interface IApiResponseTypeRegistryKeyProvider
{
    string Get(Type responseType);
}

sealed class ApiResponseTypeRegistryKeyProvider : IApiResponseTypeRegistryKeyProvider
{
    public string Get(Type responseType)
    {
        return responseType.FullName ?? throw new FailedToGetApiResponseTypeRegistryKeyException(responseType);
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