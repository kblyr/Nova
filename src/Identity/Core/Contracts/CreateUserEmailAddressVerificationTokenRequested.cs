namespace Nova.Identity.Contracts;

public record CreateUserEmailAddressVerificationTokenRequestedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}