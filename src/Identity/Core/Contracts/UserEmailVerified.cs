namespace Nova.Identity.Contracts;

public record UserEmailVerifiedEvent : INotification
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
}