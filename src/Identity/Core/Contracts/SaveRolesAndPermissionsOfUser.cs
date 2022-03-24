namespace Nova.Identity.Contracts;

public record SaveRolesAndPermissionsOfUser : Request
{
    public int UserId { get; init; }
    public RolesObj Roles { get; init; } = new(Enumerable.Empty<int>(), Enumerable.Empty<int>());
    public PermissionsObj Permissions { get; init; } = new(Enumerable.Empty<int>(), Enumerable.Empty<int>());

    public record RolesObj(IEnumerable<int> Added, IEnumerable<int> Removed);

    public record PermissionsObj(IEnumerable<int> Added, IEnumerable<int> Removed);

    public record Response(Response.RolesObj Roles, Response.PermissionsObj Permissions) : Messaging.Response
    {
        public record RolesObj(IEnumerable<int> Added, IEnumerable<int> Removed);

        public record PermissionsObj(IEnumerable<int> Added, IEnumerable<int> Removed);
    }
}