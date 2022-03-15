using Microsoft.EntityFrameworkCore;
using Nova.Auditing;

namespace Nova.Identity.Handlers;

sealed class SaveRolesAndPermissionsOfUser_Handler : RequestHandler<SaveRolesAndPermissionsOfUser>
{
    readonly IDbContextFactory<DatabaseContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;

    public SaveRolesAndPermissionsOfUser_Handler(IDbContextFactory<DatabaseContext> contextFactory, ICurrentAuditInfoProvider currentAuditInfoProvider)
    {
        _contextFactory = contextFactory;
        _currentAuditInfoProvider = currentAuditInfoProvider;
    }

    public async Task<Response> Handle(SaveRolesAndPermissionsOfUser request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (await context.Users.Exists(request.UserId) == false)
            return new UserNotFound(request.UserId);

        var auditInfo = _currentAuditInfoProvider.Current;
        var addedRoleIds = new List<int>();
        var removedRoleIds = new List<int>();
        var addedPermissionIds = new List<int>();
        var removedPermissionIds = new List<int>();

        if (request.Roles is not null)
        {
            if (request.Roles.Added?.Any() ?? false)
            {
                foreach (var id in request.Roles.Added.Distinct())
                {
                    if (await context.Roles.Exists(id) == false)
                        return new RoleNotFound(id);

                    if (await context.UserRoles.Exists(request.UserId, id))
                        continue;

                    var userRole = new UserRole
                    {
                        UserId = request.UserId,
                        RoleId = id,
                        IsDeleted = false,
                        InsertedById = auditInfo.UserId,
                        InsertedOn = auditInfo.Timestamp
                    };
                    context.UserRoles.Add(userRole);
                    addedRoleIds.Add(id);
                }
            }

            if (request.Roles.Removed?.Any() ?? false)
            {
                foreach (var id in request.Roles.Removed.Distinct())
                {
                    var userRole = await GetUserRole(context, request.UserId, id);

                    if (userRole is null)
                        continue;

                    userRole.IsDeleted = true;
                    userRole.DeletedById = auditInfo.UserId;
                    userRole.DeletedOn = auditInfo.Timestamp;
                    context.UserRoles.Update(userRole);
                    removedRoleIds.Add(id);
                }
            }

            await context.SaveChangesAsync();
        }

        if (request.Permissions is not null)
        {
            if (request.Permissions.Added?.Any() ?? false)
            {
                foreach (var id in request.Permissions.Added.Distinct())
                {
                    if (await context.Permissions.Exists(id) == false)
                        return new PermissionNotFound(id);

                    if (await UserPermissionExists(context, request.UserId, id))
                        continue;

                    var userPermission = new UserPermission
                    {
                        UserId = request.UserId,
                        PermissionId = id,
                        IsDeleted = false,
                        InsertedById = auditInfo.UserId,
                        InsertedOn = auditInfo.Timestamp
                    };
                    context.UserPermissions.Add(userPermission);
                    addedPermissionIds.Add(id);
                }
            }

            if (request.Permissions.Removed?.Any() ?? false)
            {
                foreach (var id in request.Permissions.Removed.Distinct())
                {
                    var userPermission = await GetUserPermission(context, request.UserId, id);

                    if (userPermission is null)
                        continue;

                    userPermission.IsDeleted = true;
                    userPermission.DeletedById = auditInfo.UserId;
                    userPermission.DeletedOn = auditInfo.Timestamp;
                    context.UserPermissions.Update(userPermission);
                    removedPermissionIds.Add(id);
                }
            }

            await context.SaveChangesAsync();
        }

        await transaction.CommitAsync();

        return new SaveRolesAndPermissionsOfUser.Response
        (
            new(addedRoleIds, removedRoleIds),
            new(addedPermissionIds, removedPermissionIds)
        );
    }

    static async Task<UserRole> GetUserRole(DatabaseContext context, int userId, int roleId)
    {
        return await context.UserRoles
            .Where(userRole => userRole.UserId == userId && userRole.RoleId == roleId && !userRole.IsDeleted)
            .SingleOrDefaultAsync();
    }

    static async Task<bool> UserPermissionExists(DatabaseContext context, int userId, int permissionId)
    {
        return await context.Permissions
            .AsNoTracking()
            .Where(permission =>
                permission.Id == permissionId 
                && !permission.IsDeleted
                && (
                    permission.UserPermissions.Any(userPermission => userPermission.UserId == userId && !userPermission.IsDeleted)
                    || permission.RolePermissions.Any(rolePermissions =>
                        !rolePermissions.IsDeleted
                        && rolePermissions.Role.UserRoles.Any(userRole => userRole.UserId == userId && !userRole.IsDeleted)    
                    )
                )
            )
            .AnyAsync();
    }

    static async Task<UserPermission> GetUserPermission(DatabaseContext context, int userId, int permissionId)
    {
        return await context.UserPermissions
            .Where(userPermission => userPermission.UserId == userId && userPermission.PermissionId == permissionId && !userPermission.IsDeleted)
            .SingleOrDefaultAsync();
    }
}
