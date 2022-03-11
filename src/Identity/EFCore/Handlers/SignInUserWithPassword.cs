using Microsoft.EntityFrameworkCore;
using Nova.Identity.Utilities;

namespace Nova.Identity.Handlers;

sealed class SignInUserWithPassword_Handler : RequestHandler<SignInUserWithPassword>
{
    readonly IDbContextFactory<DatabaseContext> _contextFactory;
    readonly IUserPasswordHash _passwordHash;

    public SignInUserWithPassword_Handler(IDbContextFactory<DatabaseContext> contextFactory, IUserPasswordHash passwordHash)
    {
        _contextFactory = contextFactory;
        _passwordHash = passwordHash;
    }

    public async Task<Response> Handle(SignInUserWithPassword request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var user = await GetUser(context, request.Id);

        if (user is null)
            return new UserNotFound(request.Id);

        if (user.StatusId != UserStatuses.Active)
            return new UserNotActive(user.Id, user.StatusId);

        var hashedPassword = _passwordHash.Compute(request.Password);

        if (user.HashedPassword != hashedPassword)
            return IncorrectUserPassword.Instance;

        return SignInUserWithPassword.Response.Instance;
    }

    static async Task<User> GetUser(DatabaseContext context, int id)
    {
        return await context.Users
            .AsNoTracking()
            .Where(user => user.Id == id && !user.IsDeleted)
            .Select(user => new User
            {
                HashedPassword = user.HashedPassword,
                StatusId = user.StatusId
            })
            .SingleOrDefaultAsync();
    }
}
