namespace Nova.Identity.Contracts;

public record AddApplicationToUser : Request
{
    public int UserId { get; init; }
    public short ApplicationId { get; init; }

    public record Response(long Id) : Messaging.Response;
}