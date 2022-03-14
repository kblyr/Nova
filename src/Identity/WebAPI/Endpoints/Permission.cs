namespace Nova.Identity.Endpoints;

sealed class Permission_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/permission/add", Add);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddPermission.Request request)
    {
        var response = await mediator.Send<AddPermission.Request, Contracts.AddPermission>(request);
        return Results.Extensions.Mapped(response);
    }
}