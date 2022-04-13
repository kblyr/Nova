using System.Text.Json;

namespace Nova;

public interface IApiResponseTypeSerializer
{
    IApiResponse? Deserialize<T>(Refit.ApiResponse<T> response) where T : IApiResponse;
}

sealed class ApiResponseTypeSerializer : IApiResponseTypeSerializer
{
    readonly IApiResponseTypeRegistry _registry;
    readonly JsonSerializerOptions _jsonOptions;

    public ApiResponseTypeSerializer(IApiResponseTypeRegistry registry, JsonOptionsProvider jsonOptions)
    {
        _registry = registry;
        _jsonOptions = jsonOptions.Value;
    }

    public IApiResponse? Deserialize<T>(Refit.ApiResponse<T> response) where T : IApiResponse
    {
        if (response.IsSuccessStatusCode)
            return response.Content;

        if (!response.Error.HasContent || string.IsNullOrWhiteSpace(response.Error.Content))
            return null;
        
        var errorType = GetErrorType(response);

        if (string.IsNullOrWhiteSpace(errorType))
            return null;

        if (_registry.TryGet(errorType, out Type apiResponseType))
            return JsonSerializer.Deserialize(response.Error.Content, apiResponseType, _jsonOptions) as IApiResponse;

        return null;
    }

    string GetErrorType<T>(Refit.ApiResponse<T> response)
    {
        if (!response.Headers.Contains(ApiHeaders.ResponseObjectType))
            return "";
        
        var errorTypeHeader = response.Headers.FirstOrDefault(header => header.Key == ApiHeaders.ResponseObjectType);
        return errorTypeHeader.Value.First();
    }

    public sealed class JsonOptionsProvider
    {
        public JsonSerializerOptions Value { get; }

        public JsonOptionsProvider(JsonSerializerOptions value)
        {
            Value = value;
        }
    }
}