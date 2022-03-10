namespace Nova.Identity;

public record IdentityUserForSignIn(string Username, short ApplicationId) : Request
{
    public record Response(int Id, string Username, short StatusId) : Messaging.Response;
}