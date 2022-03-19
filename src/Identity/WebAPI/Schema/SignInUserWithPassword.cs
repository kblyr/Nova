namespace Nova.Identity.Schema;

public static class SignInUserWithPassword
{
    public record Request(string Password, short ApplicationId);

    public record Response(int Id, string Username, short StatusId);
}