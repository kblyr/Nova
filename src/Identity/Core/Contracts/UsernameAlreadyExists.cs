namespace Nova.Identity.Contracts;

public record UsernameAlreadyExistsResponse : IFailedResponse
{
    public string Username { get; init; } = "";
}