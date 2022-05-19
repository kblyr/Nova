namespace Nova.Identity.Contracts;

public record EmailVerifiedEvent : INotification
{
    public string EmailAddress { get; init; } = "";
}