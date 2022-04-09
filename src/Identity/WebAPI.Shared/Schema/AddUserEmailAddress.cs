namespace Nova.Identity.Schema;

public static class AddUserEmailAddress
{
    public const string ROUTE = $"{ControllerRoutes.User}/{ActionRoutes.User.AddEmailAddress}";

    public record Request(string EmailAddress) : IApiRequest;

    public record Response(string Id) : IApiResponse;
}