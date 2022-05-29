namespace Nova.Identity.Contracts;

public record UserEmailVerifiedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}