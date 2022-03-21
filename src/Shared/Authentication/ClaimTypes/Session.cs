namespace Nova.Authentication.ClaimTypes;

public record SessionClaimType
{
    public UserObj User { get; init; } = new();
    public ApplicationObj Application { get; init; } = new();
    public DomainObj? Domain { get; init; }
    public IEnumerable<int> Roles { get; init; } = Enumerable.Empty<int>();
    public IEnumerable<int> Permissions { get; init; } = Enumerable.Empty<int>();

    public record UserObj
    {
        public int Id { get; init; }
        public string Username { get; init; } = "";
    }

    public record ApplicationObj
    {
        public short Id { get; init; }
        public string Name { get; init; } = "";
    }

    public record DomainObj
    {
        public short Id { get; init; }
        public string Name { get; init; } = "";
    }

    public const string ClaimTypeName = "Nova:Session";
}