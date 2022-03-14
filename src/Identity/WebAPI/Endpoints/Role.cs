namespace Nova.Identity.Endpoints;

sealed class Role_EndpointMapper : EndpointMapper
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/role/add", Add);
        builder.MapPost("/role/{id}/savePermissions", SavePermissions);
    }

    static async Task<IResult> Add(MappedMediator mediator, AddRole.Request request)
    {
        var response = await mediator.Send<AddRole.Request, Contracts.AddRole>(request);
        return Results.Extensions.Mapped(response);
    }
    
    static async Task<IResult> SavePermissions(MappedMediator mediator, int id, SavePermissionsOfRole.Request request)
    {
        var response = await mediator.Send<SavePermissionsOfRole.Request, Contracts.SavePermissionsOfRole>(request, request => request with { RoleId = id });
        return Results.Extensions.Mapped(response);
    }
}
