namespace Nova.Identity.Schema;

public static class AddUser
{
    public const string Route = "";

    public record Request(string Username, short StatusId);

    public record Response(string Id);
}