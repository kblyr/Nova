namespace Nova.Identity.Contracts;

public record CreateUserEmailVerificationTokenRequestedEvent : IUserEmailVerificationPayload, INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}