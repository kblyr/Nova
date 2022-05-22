namespace Nova.Identity.Contracts;

public record UserEmailVerificationCodeSentEvent : INotification
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
}