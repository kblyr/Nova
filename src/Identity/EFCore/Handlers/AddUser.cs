namespace Nova.Identity.Handlers;

sealed class AddUserHandler : IRequestHandler<AddUserCommand>
{
    readonly IDbContextFactory<UserDbContext> _contextFactory;
    readonly IMapper _mapper;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly UserStatusesConfig _userStatuses;
    readonly IMediator _mediator;

    public AddUserHandler(IDbContextFactory<UserDbContext> contextFactory, IMapper mapper, ICurrentAuditInfoProvider auditInfoProvider, IOptions<UserStatusesConfig> userStatuses, IMediator mediator)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _auditInfoProvider = auditInfoProvider;
        _userStatuses = userStatuses.Value;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (await context.UserStatuses.Exists(request.StatusId) == false)
            return new UserStatusNotFoundResponse(request.StatusId);

        if (await context.Users.UsernameExists(request.Username))
            return new UsernameAlreadyExistsResponse(request.Username);

        if (await context.UserEmailAddresses.Exists(request.EmailAddress))
            return new UserEmailAddressAlreadyExistsResponse(request.EmailAddress);

        var auditInfo = _auditInfoProvider.Current;
        var user = _mapper.Map<AddUserCommand, User>(request);
        user.IsDeleted = false;
        user.InsertedById = auditInfo.UserId;
        user.InsertedOn = auditInfo.Timestamp;
        context.Users.Add(user);
        await context.SaveChangesAsync();
        var userEmailAddress = new UserEmailAddress
        {
            UserId = user.Id,
            EmailAddress = request.EmailAddress,
            IsPrimary = true,
            IsVerified = user.StatusId == _userStatuses.Active,
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.UserEmailAddresses.Add(userEmailAddress);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        await _mediator.Publish(_mapper.Map<User, UserAddedEvent>(user));
        await _mediator.Publish(_mapper.Map<UserEmailAddress, UserEmailAddressAddedEvent>(userEmailAddress));
        return new AddUserCommand.Response(user.Id);

    }
}