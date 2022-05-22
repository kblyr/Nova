namespace Nova.Identity.Contracts;

public record UserSignedUpEvent : INotification
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
    public short StatusId { get; init; }
}