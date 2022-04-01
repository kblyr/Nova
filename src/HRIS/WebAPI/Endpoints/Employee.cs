namespace Nova.HRIS.Endpoints;

sealed class Employee_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/employee/add", Add);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddEmployee.Request request)
    {
        var response = await mediator.Send<AddEmployee.Request, Contracts.AddEmployee>(request);
        return Results.Extensions.Mapped(response);
    }
}
