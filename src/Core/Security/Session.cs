#nullable disable

namespace Nova.Security;

public record Session
{
    public int UserId { get; init; }
    public string Username { get; init; }
    public short ApplicationId { get; init; }
    public IEnumerable<int> Roles { get; init; }
    public IEnumerable<int> Permissions { get; init; }
}