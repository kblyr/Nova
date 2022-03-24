using Microsoft.EntityFrameworkCore;

namespace Nova.Identity.Handlers;

sealed class GetAccessTokenPayload_Handler : RequestHandler<GetAccessTokenPayload>
{
    readonly IDbContextFactory<DatabaseContext> _contextFactory;

    public GetAccessTokenPayload_Handler(IDbContextFactory<DatabaseContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Response> Handle(GetAccessTokenPayload request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var user = await GetUser(context, request.UserId);

        if (user is null)
            return new UserNotFound(request.UserId);

        var application = await GetApplication(context, request.ApplicationId);
            
        if (application is null)
            return new ApplicationNotFound(request.ApplicationId);
        
        if (await context.UserApplications.Exists(request.UserId, request.ApplicationId) == false)
            return new UserNotLinkedToApplication(request.UserId, request.ApplicationId);

        var roles = await GetRoles(context, request.UserId, request.ApplicationId);
        var permissions = await GetPermissions(context, request.UserId, request.ApplicationId);

        return new GetAccessTokenPayload.Response(user, application, roles, permissions);
    }

    static async Task<GetAccessTokenPayload.Response.UserObj> GetUser(DatabaseContext context, int id)
    {
        return await context.Users
            .AsNoTracking()
            .Where(user => user.Id == id && !user.IsDeleted)
            .Select(user => new GetAccessTokenPayload.Response.UserObj
            (
                user.Id,
                user.Username,
                new
                (
                    user.Status.Id, 
                    user.Status.Name
                )
            ))
            .SingleOrDefaultAsync();
    }

    static async Task<GetAccessTokenPayload.Response.ApplicationObj> GetApplication(DatabaseContext context, short id)
    {
        return await context.Applications
            .AsNoTracking()
            .Where(application => application.Id == id)
            .Select(application => new GetAccessTokenPayload.Response.ApplicationObj
            (
                application.Id,
                application.Name,
                application.Domain == null 
                    ? null
                    : new
                    (
                        application.Domain.Id,
                        application.Domain.Name
                    )
            ))
            .SingleOrDefaultAsync();
    }

    static async Task<IEnumerable<string>> GetRoles(DatabaseContext context, int userId, short applicationId)
    {
        var query = context.Roles
            .AsNoTracking()
            .Where(role =>
                !role.IsDeleted
                && role.ApplicationId == applicationId
                && role.UserRoles.Any(userRole => userRole.UserId == userId && !userRole.IsDeleted)
            )
            .Select(role => role.Code)
            .Distinct();

        return await query.ToArrayAsync() ?? Enumerable.Empty<string>();
    }

    static async Task<IEnumerable<string>> GetPermissions(DatabaseContext context, int userId, short applicationId)
    {
        var query = context.Permissions
            .AsNoTracking()
            .Where(permission =>
                !permission.IsDeleted
                && permission.ApplicationId == applicationId
                && 
                (
                    permission.UserPermissions.Any(userPermission => userPermission.UserId == userId && !userPermission.IsDeleted)
                    || permission.RolePermissions.Any(rolePermission => 
                        !rolePermission.IsDeleted
                        && !rolePermission.Role.IsDeleted
                        && rolePermission.Role.UserRoles.Any(userRole => userRole.UserId == userId && !userRole.IsDeleted)
                    )
                )
            )
            .Select(permission => permission.Code)
            .Distinct();

        return await query.ToArrayAsync() ?? Enumerable.Empty<string>();
    }
}
