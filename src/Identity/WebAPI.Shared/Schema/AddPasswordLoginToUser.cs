namespace Nova.Identity.Schema;

public static class AddPasswordLoginToUser
{
    public const string ROUTE = $"{ControllerRoutes.User}/{ActionRoutes.User.AddPasswordLogin}";

    [SchemaId(SchemaIds.AddPasswordLoginToUser.Request)]
    public record Request(string SecurePassword) : IApiRequest;

    [SchemaId(SchemaIds.AddPasswordLoginToUser.Response)]
    public record Response(string Id) : IApiResponse;
}