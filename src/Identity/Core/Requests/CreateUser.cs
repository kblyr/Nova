namespace Nova.Identity.Requests;

public record CreateUser
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
    public bool IsActive { get; init; }
    public IEnumerable<short> DomainIds { get; init; } = default!;
    public IEnumerable<int> RoleIds { get; init; } = default!;
    public IEnumerable<int> PermissionIds { get; init; } = default!;

    
}