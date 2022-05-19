namespace Nova.Identity.Contracts;

public record EmailVerificationCodeSentEvent : INotification
{
    public string EmailAddress { get; init; } = "";
}