namespace Nova.Identity.Contracts;

public record GenerateRefreshToken(GenerateRefreshToken.AccessTokenObj AccessToken) : Request
{
    public record AccessTokenObj(string Id, string TokenString);

    public record Response(string RefreshToken) : Messaging.Response;
}