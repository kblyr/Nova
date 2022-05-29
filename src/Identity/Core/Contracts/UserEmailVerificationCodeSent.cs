namespace Nova.Identity.Contracts;

public record UserEmailVerificationCodeSentEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}