namespace Nova.Identity.Contracts;

public record CreateUserEmailAddressVerificationLinkRequestedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string TokenString { get; init; } = "";
}