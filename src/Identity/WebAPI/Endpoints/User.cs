using System.Net.Mime;

namespace Nova.Identity.Endpoints;

sealed class User_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/user/add", Add)
            .Accepts<AddUser.Request>(MediaTypeNames.Application.Json)
            .Produces<AddUser.Response>(StatusCodes.Status201Created, MediaTypeNames.Application.Json);

        builder.MapPost("/user/signIn", IdentifyForSignIn)
            .Accepts<IdentifyUserForSignIn.Request>(MediaTypeNames.Application.Json)
            .Produces<IdentifyUserForSignIn.Response>(StatusCodes.Status200OK, MediaTypeNames.Application.Json);

        builder.MapPost("/user/signIn/{id}/password", SignInWithPassword);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddUser.Request request)
    {
        var response = await mediator.Send<AddUser.Request, Contracts.AddUser>(request);
        return Results.Extensions.Mapped(response);
    }

    static async Task<IResult> IdentifyForSignIn(MappedMediator mediator, IdentifyUserForSignIn.Request request)
    {
        var response = await mediator.Send<IdentifyUserForSignIn.Request, Contracts.IdentifyUserForSignIn>(request);
        return Results.Extensions.Mapped(response);
    }

    static async Task<IResult> SignInWithPassword(MappedMediator mediator, int id, SignInUserWithPassword.Request request)
    {
        var response = await mediator.Send<SignInUserWithPassword.Request, Contracts.SignInUserWithPassword>(request, request => request with { Id = id });
        return Results.Extensions.Mapped(response);
    }
}