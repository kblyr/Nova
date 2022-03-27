namespace Nova.Authentication;

public record Session
{
    public int UserId { get; init; }
    public string Username { get; init; } = "";
    public short ApplicationId { get; init; }
    public short? DomainId { get; init; }
    public IEnumerable<int> Roles { get; init; } = Enumerable.Empty<int>();
    public IEnumerable<int> Permissions { get; init; } = Enumerable.Empty<int>();

    public const string ClaimType = "NovaSession";
}