namespace Nova.Contracts;

public record EmailVerificationCodeSentEvent : INotification
{
    public string EmailAddress { get; init; } = "";
}