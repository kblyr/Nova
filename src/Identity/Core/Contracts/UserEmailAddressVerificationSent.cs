namespace Nova.Identity.Contracts;

public record UserEmailAddressVerificationSentEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}