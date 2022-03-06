using System.Net.Mime;

namespace Nova.Identity.Endpoints;

sealed class User_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/user/add", Add)
            .Accepts<AddUser.Request>(MediaTypeNames.Application.Json)
            .Produces<AddUser.Response>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
            .Produces<ApiFailedResponse>(StatusCodes.Status400BadRequest);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddUser.Request request)
    {
        var response = await mediator.Send<AddUser.Request, Contracts.AddUser>(request);
        return Results.Extensions.Mapped(response);
    }
}