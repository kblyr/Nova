namespace Nova.Identity.Contracts;

public record GenerateAccessToken(int UserId, short ApplicationId) : Request
{
    public record Response(Response.AccessTokenObj AccessToken) : Messaging.Response
    {
        public record AccessTokenObj(Guid Id, string TokenString);
    }
}