namespace Nova.Identity.Contracts;

public record IdentifyUserForSignIn(string Username, short ApplicationId) : Request
{
    public record Response(int Id, string Username, short StatusId) : Messaging.Response;
}