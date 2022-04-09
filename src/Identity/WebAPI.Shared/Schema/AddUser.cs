namespace Nova.Identity.Schema;

public static class AddUser
{
    public const string ROUTE = $"{ControllerRoutes.User}/{ActionRoutes.User.Add}";

    public record Request(string Username, string EmailAddress, short StatusId) : IApiRequest;

    public record Response(string Id) : IApiResponse;
}