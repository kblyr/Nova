namespace Nova.Identity.Endpoints;

sealed class Role_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/role/add", Add);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddRole.Request request)
    {
        var response = await mediator.Send<AddRole.Request, Contracts.AddRole>(request);
        return Results.Extensions.Mapped(response);
    }
}
