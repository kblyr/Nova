namespace Nova.Identity.Schema;

public static class AddUserPasswordLogin
{
    public const string ROUTE = $"{ControllerRoutes.User}/{ActionRoutes.User.AddPasswordLogin}";

    public record Request(string SecurePassword) : IApiRequest;

    public record Response(string Id) : IApiResponse;
}