namespace Nova.Identity.Contracts;

public record UserEmailAddressVerificationLinkCreatedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string Link { get; init; } = "";
}