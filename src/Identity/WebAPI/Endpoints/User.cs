namespace Nova.Identity.Endpoints;

sealed class User_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/user/add", Add);
    }

    static async Task<IResult> Add(MappedMediator mediator, ResponseMapper responseMapper, AddUser.Request request)
    {
        var response = await mediator.Send<AddUser.Request, Contracts.AddUser>(request);
        return Results.Ok(responseMapper.Map<Contracts.AddUser.Response, AddUser.Response>(response));
    }
}