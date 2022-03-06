using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Nova.Messaging;

public class ResponseMapper
{
    readonly IMapper _mapper;
    readonly ResponseTypeRegistry _registry;

    public ResponseMapper(IMapper mapper, ResponseTypeRegistry registry)
    {
        _mapper = mapper;
        _registry = registry;
    }

    public (object? Data, int StatusCode) Map(Response response)
    {
        if (response is null)
            return (null, StatusCodes.Status204NoContent);

        var responseType = response.GetType();
        var responseTypeDefinition = _registry.ApiResponseFor(responseType);

        if (!responseTypeDefinition.HasValue)
            throw new InvalidOperationException($"No API response type registered for type '{responseType.FullName}'");

        var mappedData = _mapper.Map(response, responseType, responseTypeDefinition.Value.ApiResponseType);

        return 
        (
            response is FailedResponse ? new ApiFailedResponse(responseType.Name, mappedData) : mappedData,
            responseTypeDefinition.Value.StatusCode
        );
    }
}