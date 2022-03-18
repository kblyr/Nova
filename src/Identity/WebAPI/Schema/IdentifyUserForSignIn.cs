namespace Nova.Identity.Schema;

public static class IdentifyUserForSignIn
{
    public record Request(string Username, short ApplicationId);

    public record Response(int Id, string Username, short StatusId);
}