namespace Nova.Identity.Schema;

public static class AddUser
{
    public const string ROUTE = "";

    public record Request(string Username, string EmailAddress, short StatusId);

    public record Response(string Id);
}