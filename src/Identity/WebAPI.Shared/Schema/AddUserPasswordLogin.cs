namespace Nova.Identity.Schema;

public static class AddUserPasswordLogin
{
    public const string ROUTE = "{id}/password-login";

    public record Request(string SecurePassword);

    public record Response(string Id);
}