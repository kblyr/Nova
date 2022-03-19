namespace Nova.Identity.Schema;

public static class SaveRolesAndPermissionsOfUser
{
    public record Request(Request.RolesObj Roles, Request.PermissionsObj Permissions)
    {
        public record RolesObj(IEnumerable<int> Added, IEnumerable<int> Removed);

        public record PermissionsObj(IEnumerable<int> Added, IEnumerable<int> Removed);
    }

    public record Response(Response.RolesObj Roles, Response.PermissionsObj Permissions)
    {
        public record RolesObj(IEnumerable<int> Added, IEnumerable<int> Removed);

        public record PermissionsObj(IEnumerable<int> Added, IEnumerable<int> Removed);
    }
}