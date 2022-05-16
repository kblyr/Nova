namespace Nova.Identity.Contracts;

public record SendUserCreationConfirmationMailRequestedEvent : INotification
{
    public int UserId { get; init; }
    public string EmailAddress { get; init; } = "";
    public string? Password { get; init; }
}