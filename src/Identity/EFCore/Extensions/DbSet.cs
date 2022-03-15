using Microsoft.EntityFrameworkCore;

namespace Nova.Identity;

public static class DbSet_Extensions
{
    public static async Task<bool> UsernameExists(this DbSet<User> users, string username)
    {
        return await users
            .AsNoTracking()
            .Where(user => user.Username == username && !user.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<UserStatus> userStatuses, short id)
    {
        return await userStatuses
            .AsNoTracking()
            .Where(userStatus => userStatus.Id == id)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Application> applications, short id)
    {
        return await applications
            .AsNoTracking()
            .Where(application => application.Id == id)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<UserApplication> userApplications, int userId, short applicationId)
    {
        return await userApplications
            .AsNoTracking()
            .Where(userApplication => userApplication.UserId == userId && userApplication.ApplicationId == applicationId && !userApplication.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Domain> domains, short id)
    {
        return await domains
            .AsNoTracking()
            .Where(domain => domain.Id == id)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Role> roles, string name, short? domainId, short? applicationId)
    {
        return await roles
            .AsNoTracking()
            .Where(role =>
                !role.IsDeleted
                && role.Name == name
                && role.DomainId == domainId
                && role.ApplicationId == applicationId 
            )
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Permission> permissions, string name, short? domainId, short? applicationId)
    {
        return await permissions
            .AsNoTracking()
            .Where(permission =>
                !permission.IsDeleted
                && permission.Name == name
                && permission.DomainId == domainId
                && permission.ApplicationId == applicationId
            )
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<RolePermission> rolePermissions, int roleId, int permissionId)
    {
        return await rolePermissions
            .AsNoTracking()
            .Where(rolePermission =>
                !rolePermission.IsDeleted
                && rolePermission.RoleId == roleId
                && rolePermission.PermissionId == permissionId
            )
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<User> users, int id)
    {
        return await users
            .AsNoTracking()
            .Where(user => user.Id == id && !user.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Role> roles, int id)
    {
        return await roles
            .AsNoTracking()
            .Where(role => role.Id == id && !role.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<UserRole> userRoles, int userId, int roleId)
    {
        return await userRoles
            .AsNoTracking()
            .Where(userRole => userRole.UserId == userId && userRole.RoleId == roleId && !userRole.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<Permission> permissions, int id)
    {
        return await permissions
            .AsNoTracking()
            .Where(permission => permission.Id == id && !permission.IsDeleted)
            .AnyAsync();
    }

    public static async Task<bool> Exists(this DbSet<UserPermission> userPermissions, int userId, int permissionId)
    {
        return await userPermissions
            .AsNoTracking()
            .Where(userPermission => userPermission.UserId == userId && userPermission.PermissionId == permissionId && !userPermission.IsDeleted)
            .AnyAsync();
    }
}