namespace Nova.Identity.Contracts;

public record SendUserCreationConfirmationMailRequestedEvent : INotification
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
    public string? CipherPassword { get; init; }
}