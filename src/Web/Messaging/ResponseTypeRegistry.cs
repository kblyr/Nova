using System.Reflection.Metadata;

namespace Nova.Messaging;

public class ResponseTypeRegistry
{
    static readonly Dictionary<Type, ResponseTypeDefinition> _responseTypes = new();

    public ResponseTypeDefinition? ApiResponseFor(Type responseType)
    {
        return _responseTypes[responseType];
    }

    public ResponseTypeDefinition? ApiResponseFor<TResponse>() where TResponse : Response
    {
        return ApiResponseFor(typeof(TResponse));
    }

    public ResponseTypeRegistry Register<TResponse, TApiResponse>(int statusCode) where TResponse : Response
    {
        var responseType = typeof(TResponse);

        if (_responseTypes.ContainsKey(responseType))
            return this;
        
        _responseTypes.Add(responseType, new(typeof(TApiResponse), statusCode));
        return this;
    }
}

public record struct ResponseTypeDefinition
(
    Type ApiResponseType,
    int StatusCode
);