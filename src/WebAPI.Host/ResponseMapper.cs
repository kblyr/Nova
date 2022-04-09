using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Nova;

public interface IResponseMapper
{
    ObjectResult Map(IResponse response);
}

sealed class ResponseMapper : IResponseMapper
{
    readonly IResponseTypeMapRegistry _registry;
    readonly IApiResponseTypeRegistryKeyProvider _registryKeyProvider;
    readonly IMapper _mapper;
    readonly IHttpContextAccessor _contextAccessor;

    public ResponseMapper(IResponseTypeMapRegistry registry, IApiResponseTypeRegistryKeyProvider registryKeyProvider, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _registry = registry;
        _registryKeyProvider = registryKeyProvider;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public ObjectResult Map(IResponse response)
    {
        var isFailedResponse = response is IFailedResponse;
        
        if (!_registry.TryGet(response.GetType(), out ResponseTypeMapDefinition definition))
            return new ObjectResult(null) { StatusCode = StatusCodes.Status204NoContent };

        _contextAccessor.HttpContext?.Response.Headers.Add(ApiHeaders.ErrorType, _registryKeyProvider.Get(definition.ApiResponseType));
        return new(_mapper.Map(response, definition.ResponseType, definition.ApiResponseType)) { StatusCode = definition.StatusCode };
    }
}
