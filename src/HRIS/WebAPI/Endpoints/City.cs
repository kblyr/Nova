namespace Nova.HRIS.Endpoints;

sealed class City_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/city/add", Add);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddCity.Request request)
    {
        var response = await mediator.Send<AddCity.Request, Contracts.AddCity>(request);
        return Results.Extensions.Mapped(response);
    }
}
