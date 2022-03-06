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

    public ResponseTypeRegistry Register<TResponse, TApiResponse>(int statusCode, Action<ResponseTypeDefinitionMetadata>? configure = null) where TResponse : Response
    {
        var responseType = typeof(TResponse);

        if (_responseTypes.ContainsKey(responseType))
            return this;

        var metadata = new ResponseTypeDefinitionMetadata();
        configure?.Invoke(metadata);
        _responseTypes.Add(responseType, new(typeof(TApiResponse), statusCode, metadata));
        return this;
    }
}

public record struct ResponseTypeDefinition
(
    Type ApiResponseType,
    int StatusCode,
    ResponseTypeDefinitionMetadata Metadata
);

public class ResponseTypeDefinitionMetadata
{
    readonly Dictionary<string, object?> _source = new();

    public object? this[string name]
    {
        get
        {
            if (name is null || !_source.ContainsKey(name))
                return null;

            return _source[name];
        }
        set
        {
            if (name is null)
                return;

            if (_source.ContainsKey(name))
                _source[name] = value;
            else
                _source.Add(name, value);
        }
    }
}