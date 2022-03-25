namespace Nova.Identity.Configuration;

public record PermissionsConfig
{
    public int AddPermission { get; init; }
    public int AddRole { get; init; }
    public int AddRolePermission { get; init; }
    public int AddUser { get; init; }
    public int AddUserApplication { get; init; }
    public int AddUserPermission { get; init; }
    public int AddUserRole { get; init; }
    public int DeleteRolePermission { get; init; }
    public int DeleteUserPermission { get; init; }
    public int DeleteUserRole { get; init; }

    public const string ConfigKey = "Permissions";
}