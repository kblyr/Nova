using Microsoft.EntityFrameworkCore;

namespace Nova.Identity.Handlers;

sealed class GetUserStatuses_Handler : RequestHandler<GetUserStatuses>
{
    readonly IDbContextFactory<UserStatusDbContext> _contextFactory;

    public GetUserStatuses_Handler(IDbContextFactory<UserStatusDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Response> Handle(GetUserStatuses request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var query = context.UserStatuses.AsNoTracking()
            .OrderBy(userStatus => userStatus.Name);
        var userStatuses = await query
            .Select(userStatus => new GetUserStatuses.Response.UserStatusObj(userStatus.Id, userStatus.Name))
            .ToArrayAsync();

        return new GetUserStatuses.Response(userStatuses);
    }
}
