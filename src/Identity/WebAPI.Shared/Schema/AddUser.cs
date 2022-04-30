namespace Nova.Identity.Schema;

public static class AddUser
{
    public const string ROUTE = $"/{ControllerRoutes.User}/{ActionRoutes.User.Add}";

    [SchemaId(SchemaIds.AddUser.Request)]
    public record Request(string Username, string EmailAddress, short StatusId) : IApiRequest;
    
    [SchemaId(SchemaIds.AddUser.Response)]
    public record Response(string Id) : IApiResponse;
}