namespace Nova.Identity.Schema;

public static class AddUser
{
    public record Request(string Username, string Password, short StatusId);

    public record Response(int Id);
}