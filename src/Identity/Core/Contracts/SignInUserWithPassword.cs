namespace Nova.Identity.Contracts;

public record SignInUserWithPassword : Request
{
    public int Id { get; init; }
    public string Password { get; init; } = "";
    public short ApplicationId { get; init; }

    public record Response(int Id, string Username, short StatusId) : Messaging.Response;
}