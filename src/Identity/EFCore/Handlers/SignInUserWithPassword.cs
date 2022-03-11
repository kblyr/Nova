using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nova.Identity.Utilities;

namespace Nova.Identity.Handlers;

sealed class SignInUserWithPassword_Handler : RequestHandler<SignInUserWithPassword>
{
    readonly IDbContextFactory<DatabaseContext> _contextFactory;
    readonly IUserPasswordHash _passwordHash;
    readonly IMapper _mapper;

    public SignInUserWithPassword_Handler(IDbContextFactory<DatabaseContext> contextFactory, IUserPasswordHash passwordHash, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _passwordHash = passwordHash;
        _mapper = mapper;
    }

    public async Task<Response> Handle(SignInUserWithPassword request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        if (await context.Applications.Exists(request.ApplicationId) == false)
            return new ApplicationNotFound(request.ApplicationId);

        var user = await GetUser(context, request.Id);

        if (user is null)
            return new UserNotFound(request.Id);

        if (user.StatusId != UserStatuses.Active)
            return new UserNotActive(user.Id, user.StatusId);

        if (await context.UserApplications.Exists(request.Id, request.ApplicationId) == false)
            return new UserNotLinkedToApplication(request.Id, request.ApplicationId);

        var hashedPassword = _passwordHash.Compute(request.Password);

        if (user.HashedPassword != hashedPassword)
            return IncorrectUserPassword.Instance;

        return _mapper.Map<User, SignInUserWithPassword.Response>(user);
    }

    static async Task<User> GetUser(DatabaseContext context, int id)
    {
        return await context.Users
            .AsNoTracking()
            .Where(user => user.Id == id && !user.IsDeleted)
            .Select(user => new User
            {
                Id = user.Id,
                Username = user.Username,
                HashedPassword = user.HashedPassword,
                StatusId = user.StatusId
            })
            .SingleOrDefaultAsync();
    }
}
