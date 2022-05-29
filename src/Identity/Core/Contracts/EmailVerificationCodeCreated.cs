namespace Nova.Identity.Contracts;

public record EmailVerificationCodeCreatedEvent : INotification
{
    public string EmailAddress { get; init; } = "";
    public string VerificationCode { get; init; } = "";
}