using Microsoft.EntityFrameworkCore;
using Nova.Auditing;

namespace Nova.Identity.Handlers;

sealed class SavePermissionsOfRole_Handler : RequestHandler<SavePermissionsOfRole>
{
    readonly IDbContextFactory<DatabaseContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _currentAuditInfoProvider;

    public SavePermissionsOfRole_Handler(IDbContextFactory<DatabaseContext> contextFactory, ICurrentAuditInfoProvider currentAuditInfoProvider)
    {
        _contextFactory = contextFactory;
        _currentAuditInfoProvider = currentAuditInfoProvider;
    }

    public async Task<Response> Handle(SavePermissionsOfRole request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        var role = await GetRole(context, request.RoleId);

        if (role is null)
            return new RoleNotFound(request.RoleId);

        var auditInfo = _currentAuditInfoProvider.Current;
        var addedIds = new List<int>();
        var removedIds = new List<int>();

        if (request.AddedIds?.Any() ?? false)
        {
            foreach (var id in request.AddedIds)
            {
                var permission = await GetPermission(context, id);

                if (permission is null)
                    return new PermissionNotFound(id);

                if (await context.RolePermissions.Exists(request.RoleId, id))
                    continue;

                if (role.DomainId != permission.DomainId)
                    return new PermissionNotInDomain(id, role.DomainId);

                if (role.ApplicationId != permission.ApplicationId)
                    return new PermissionNotInApplication(id, role.ApplicationId);

                var rolePermission = new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                    IsDeleted = false,
                    InsertedById = auditInfo.UserId,
                    InsertedOn = auditInfo.Timestamp
                };
                context.RolePermissions.Add(rolePermission);
                addedIds.Add(id);
            }
        }

        if (request.RemovedIds?.Any() ?? false)
        {
            foreach (var id in request.RemovedIds)
            {
                var rolePermission = await GetRolePermission(context, role.Id, id);

                if (rolePermission is null)
                    continue;

                rolePermission.IsDeleted = true;
                rolePermission.DeletedById = auditInfo.UserId;
                rolePermission.DeletedOn = auditInfo.Timestamp;
                context.RolePermissions.Update(rolePermission);
                removedIds.Add(id);
            }
        }

        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        
        return new SavePermissionsOfRole.Response(addedIds, removedIds);
    }

    static async Task<Role> GetRole(DatabaseContext context, int id)
    {
        return await context.Roles
            .AsNoTracking()
            .Where(role => role.Id == id && !role.IsDeleted)
            .Select(role => new Role
            {
                Id = role.Id,
                DomainId = role.DomainId,
                ApplicationId = role.ApplicationId
            })
            .SingleOrDefaultAsync();
    }

    static async Task<Permission> GetPermission(DatabaseContext context, int id)
    {
        return await context.Permissions
            .AsNoTracking()
            .Where(permission => permission.Id == id && !permission.IsDeleted)
            .Select(permission => new Permission
            {
                Id = permission.Id,
                DomainId = permission.DomainId,
                ApplicationId = permission.ApplicationId
            })
            .SingleOrDefaultAsync();
    }

    static async Task<RolePermission> GetRolePermission(DatabaseContext context, int roleId, int permissionId)
    {
        return await context.RolePermissions
            .Where(rolePermission => 
                !rolePermission.IsDeleted
                && rolePermission.RoleId == roleId
                && rolePermission.PermissionId == permissionId
            )
            .SingleOrDefaultAsync();
    }
}
