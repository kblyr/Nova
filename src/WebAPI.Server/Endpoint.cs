namespace Nova;

public class ApiEndpoint<TApiRequest> : Endpoint<TApiRequest> where TApiRequest : IApiRequest, new()
{
    IApiMediator? _mediator;
    protected IApiMediator Mediator => _mediator ??= Resolve<IApiMediator>();

    protected async Task Send<TRequest>(TApiRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        var response = await Mediator.Send<TApiRequest, TRequest>(request, cancellationToken);
        await SendApiResponse(response, cancellationToken);
    }

    protected async Task Send<TRequest>(TApiRequest request, MutateRequest<TRequest> mutateRequest, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        var response = await Mediator.Send<TApiRequest, TRequest>(request, mutateRequest, cancellationToken);
        await SendApiResponse(response, cancellationToken);
    }

    async Task SendApiResponse(IResponse response, CancellationToken cancellationToken)
    {
        var registry = Resolve<IResponseTypeMapRegistry>();
        var registryKeyProvider = Resolve<IApiResponseTypeRegistryKeyProvider>();
        var mapper = Resolve<IMapper>();

        if (response is null || registry.TryGet(response.GetType(), out ResponseTypeMapDefinition definition))
        {
            await SendNoContentAsync(cancellationToken);
            return;
        }

        HttpContext.Response.Headers.Add(ApiHeaders.ResponseObjectType, registryKeyProvider.Get(definition.ApiResponseType));
        await SendAsync(mapper.Map(response, definition.ResponseType, definition.ApiResponseType), definition.StatusCode, cancellationToken);
    }
}

public class ApiEndpoint<TApiRequest, TRequest> : ApiEndpoint<TApiRequest>
    where TApiRequest : IApiRequest, new()
    where TRequest : IRequest
{
    public override async Task HandleAsync(TApiRequest req, CancellationToken ct)
    {
        await Send<TRequest>(req, ct);
    }
}