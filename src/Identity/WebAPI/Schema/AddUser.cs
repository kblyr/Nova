namespace Nova.Identity.Schema;

public static class AddUser
{
    public record Request(string Username, string Password);

    public record Response(int Id);
}