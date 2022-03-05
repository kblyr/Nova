using AutoMapper;
using MediatR;
using Nova.Identity.Schema;

namespace Nova.Identity.Endpoints;

sealed class User_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/user/add", Add);
    }

    static async Task<IResult> Add(IMediator mediator, IMapper mapper, AddUser.Request request)
    {
        var response = await mediator.Send(mapper.Map<AddUser.Request, Contracts.AddUser>(request));

        if (response is Contracts.AddUser.Response successResponse)
            return Results.Created($"/user/{successResponse.Id}", successResponse);

        return Results.BadRequest();
    }
}