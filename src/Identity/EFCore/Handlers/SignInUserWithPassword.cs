using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nova.Identity.Utilities;

namespace Nova.Identity.Handlers;

sealed class SignInUserWithPassword_Handler : Messaging.RequestHandler<SignInUserWithPassword>
{
    readonly IDbContextFactory<UserDbContext> _contextFactory;
    readonly IUserPasswordHash _passwordHash;
    readonly IMapper _mapper;
    readonly IMediator _mediator;

    public SignInUserWithPassword_Handler(IDbContextFactory<UserDbContext> contextFactory, IUserPasswordHash passwordHash, IMapper mapper, IMediator mediator)
    {
        _contextFactory = contextFactory;
        _passwordHash = passwordHash;
        _mapper = mapper;
        _mediator = mediator;
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

        await _mediator.Publish(new UserSignedIn(request.Id, request.ApplicationId));
        return _mapper.Map<User, SignInUserWithPassword.Response>(user);
    }

    static async Task<User> GetUser(UserDbContext context, int id)
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
