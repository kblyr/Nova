namespace Nova.Identity.Contracts;

public record UserEmailAddressAddedEvent : INotification
{
    public long Id { get; init; }
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public short StatusId { get; init; }
}