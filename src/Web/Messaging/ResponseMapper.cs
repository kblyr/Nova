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

        var isFailedResponse = response is FailedResponse;
        var responseType = response.GetType();
        var responseTypeDefinition = _registry.ApiResponseFor(responseType);
        var mappedData = responseTypeDefinition.HasValue && responseTypeDefinition.Value.ApiResponseType is not null 
            ? _mapper.Map(response, responseType, responseTypeDefinition.Value.ApiResponseType) 
            : response;

        return 
        (
            isFailedResponse ? new ApiFailedResponse(responseType.Name, mappedData) : mappedData,
            responseTypeDefinition?.StatusCode ?? (isFailedResponse ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK)
        );
    }
}