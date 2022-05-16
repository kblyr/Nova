namespace Nova.Identity.Contracts;

public record UserCreationConfirmationMailSentEvent :  INotification
{
    public int Id { get; init; }
    public string EmailAddress { get; init; } = "";
}