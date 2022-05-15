namespace Nova.Identity.Contracts;

public record UserEmailAddressVerificationTokenCreatedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string TokenString { get; init; } = "";
}