namespace Nova.Identity.Contracts;

public record GenerateRefreshToken(GenerateRefreshToken.AccessTokenObj AccessToken) : Request
{
    public record AccessTokenObj(Guid Id, string TokenString);

    public record Response(string RefreshToken) : Messaging.Response;
}