using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Nova.Identity.Handlers;

sealed class IdentityUserForSignIn_Handler : RequestHandler<IdentityUserForSignIn>
{
    readonly IDbContextFactory<DatabaseContext> _contextFactory;
    readonly IMapper _mapper;

    public IdentityUserForSignIn_Handler(IDbContextFactory<DatabaseContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<Response> Handle(IdentityUserForSignIn request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        if (await context.Applications.Exists(request.ApplicationId) == false)
            return new ApplicationNotFound(request.ApplicationId);
        
        var user = await GetUser(context, request.Username);

        if (user is null)
            return new UsernameNotFound(request.Username);

        if (user.StatusId != UserStatuses.Active)
            return new UserNotActive(user.Id, user.StatusId);

        if (await context.UserApplications.Exists(user.Id, request.ApplicationId) == false)
            return new UserNotLinkedToApplication(user.Id, request.ApplicationId);

        return _mapper.Map<User, IdentityUserForSignIn.Response>(user);
    }

    static async Task<User> GetUser(DatabaseContext context, string username)
    {
        return await context.Users
            .AsNoTracking()
            .Where(user =>
                user.Username == username
                && !user.IsDeleted
            )
            .SingleOrDefaultAsync();
    }
}
