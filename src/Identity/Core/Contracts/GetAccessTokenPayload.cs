namespace Nova.Identity.Contracts;

public record GetAccessTokenPayload
(
    int UserId,
    short ApplicationId
) : Request
{
    public record Response : Messaging.Response;
}