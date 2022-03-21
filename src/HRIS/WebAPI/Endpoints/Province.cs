namespace Nova.HRIS.Endpoints;

sealed class Province_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/province/add", Add);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddProvince.Request request)
    {
        var response = await mediator.Send<AddProvince.Request, Contracts.AddProvince>(request);
        return Results.Extensions.Mapped(response);
    }
}
