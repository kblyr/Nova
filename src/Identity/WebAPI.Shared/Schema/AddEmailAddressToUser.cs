namespace Nova.Identity.Schema;

public static class AddEmailAddressToUser
{
    public const string ROUTE = $"{ControllerRoutes.User}/{ActionRoutes.User.AddEmailAddress}";

    [SchemaId(SchemaIds.AddEmailAddressToUser.Request)]
    public record Request(string EmailAddress) : IApiRequest;

    [SchemaId(SchemaIds.AddEmailAddressToUser.Response)]
    public record Response(string Id) : IApiResponse;
}