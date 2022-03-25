namespace Nova.Authentication;

public record Session
(
    int UserId,
    string Username,
    short ApplicationId,
    short? DomainId,
    IEnumerable<int> Roles,
    IEnumerable<int> Permissions
)
{
    public const string ClaimType = "Nova:Session";
}