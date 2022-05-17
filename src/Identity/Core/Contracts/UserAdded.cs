namespace Nova.Identity.Contracts;

public record UserAddedEvent : INotification
{
    public int Id { get; init; }
    public string Username { get; init; } = "";
    public string EmailAddress { get; init; } = "";
    public short StatusId { get; init; }
    public string? CipherPassword { get; init; }
}