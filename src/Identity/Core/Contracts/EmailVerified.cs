namespace Nova.Contracts;

public record EmailVerifiedEvent : INotification
{
    public string EmailAddress { get; init; } = "";
}