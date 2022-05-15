namespace Nova.Identity.Contracts;

public record SendUserEmailAddressVerificationRequestedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string Link { get; init; } = "";
}