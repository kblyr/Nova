namespace Nova;

public interface IResponseMapper
{
    IActionResult Map(IResponse response);
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

    public IActionResult Map(IResponse response)
    {
        if (response is null || !_registry.TryGet(response.GetType(), out ResponseTypeMapDefinition definition))
            return new ObjectResult(null) { StatusCode = StatusCodes.Status204NoContent };

        _contextAccessor.HttpContext?.Response.Headers.Add(ApiHeaders.ResponseObjectType, _registryKeyProvider.Get(definition.ApiResponseType));
        return new ObjectResult(_mapper.Map(response, definition.ResponseType, definition.ApiResponseType)) { StatusCode = definition.StatusCode };
    }
}