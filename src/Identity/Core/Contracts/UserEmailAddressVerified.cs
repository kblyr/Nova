namespace Nova.Identity.Contracts;

public record UserEmailAddressVerifiedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
}