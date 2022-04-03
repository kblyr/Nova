using AutoMapper;

namespace Nova.Identity.Handlers;

sealed class AddUserHandler : IRequestHandler<AddUserCommand>
{
    readonly IDbContextFactory<UserDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public AddUserHandler(IDbContextFactory<UserDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<IResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (await context.UserStatuses.Exists(request.StatusId) == false)
            return new UserStatusNotFoundResponse(request.StatusId);

        if (await context.Users.UsernameExists(request.Username))
            return new UsernameAlreadyExistsResponse(request.Username);

        if (await context.Users.EmailAddressExists(request.EmailAddress))
            return new UserEmailAddressAlreadyExistsResponse(request.EmailAddress);

        var auditInfo = _auditInfoProvider.Current;
        var user = _mapper.Map<AddUserCommand, User>(request);
        user.IsDeleted = false;
        user.InsertedById = auditInfo.UserId;
        user.InsertedOn = auditInfo.Timestamp;
        context.Users.Add(user);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new AddUserCommand.Response(user.Id);

    }
}