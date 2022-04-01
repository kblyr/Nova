namespace Nova.HRIS.Endpoints;

sealed class Barangay_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/barangay/add", Add);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddBarangay.Request request)
    {
        var response = await mediator.Send<AddBarangay.Request, Contracts.AddBarangay>(request);
        return Results.Extensions.Mapped(response);
    }
}
