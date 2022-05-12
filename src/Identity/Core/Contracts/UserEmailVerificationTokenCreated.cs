namespace Nova.Identity.Contracts;

public record UserEmailVerificationTokenCreatedEvent : IUserEmailVerificationPayloadWithToken, INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string TokenString { get; init; } = "";
    public DateTimeOffset ResendOn { get; init; }
}