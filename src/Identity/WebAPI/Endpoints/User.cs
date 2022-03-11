using System.Net.Mime;

namespace Nova.Identity.Endpoints;

sealed class User_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/user/add", Add)
            .Accepts<AddUser.Request>(MediaTypeNames.Application.Json)
            .Produces<AddUser.Response>(StatusCodes.Status201Created, MediaTypeNames.Application.Json);

        builder.MapPost("/user/identifyForSignIn", IdentifyForSignIn)
            .Accepts<IdentifyUserForSignIn.Request>(MediaTypeNames.Application.Json)
            .Produces<IdentifyUserForSignIn.Response>(StatusCodes.Status200OK, MediaTypeNames.Application.Json);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddUser.Request request)
    {
        var response = await mediator.Send<AddUser.Request, Contracts.AddUser>(request);
        return Results.Extensions.Mapped(response);
    }

    static async Task<IResult> IdentifyForSignIn(MappedMediator mediator, Schema.IdentifyUserForSignIn.Request request)
    {
        var response = await mediator.Send<IdentifyUserForSignIn.Request, Contracts.IdentifyUserForSignIn>(request);
        return Results.Extensions.Mapped(response);
    }
}