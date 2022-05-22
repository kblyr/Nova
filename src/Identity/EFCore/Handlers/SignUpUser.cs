using Nova.Identity.Security;

namespace Nova.Identity.Handlers;

sealed class SignUpUserHandler : IRequestHandler<SignUpUserCommand>
{
    readonly IDbContextFactory<IdentityDbContext> _contextFactory;
    readonly IUserPasswordHashAlgorithm _passwordHashAlgorithm;
    readonly UserStatusesLookup _userStatuses;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;

    public SignUpUserHandler(IDbContextFactory<IdentityDbContext> contextFactory, IUserPasswordHashAlgorithm passwordHashAlgorithm, IOptions<UserStatusesLookup> userStatuses, ICurrentAuditInfoProvider auditInfoProvider)
    {
        _contextFactory = contextFactory;
        _passwordHashAlgorithm = passwordHashAlgorithm;
        _userStatuses = userStatuses.Value;
        _auditInfoProvider = auditInfoProvider;
    }

    public async Task<IResponse> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        if (await DoesEmailAddressExists(context, request.EmailAddress, cancellationToken))
        {
            return request.Adapt<SignUpUserCommand, UserEmailAddressAlreadyExistsResponse>();
        }

        var auditInfo = _auditInfoProvider.Current;
        var password = _passwordHashAlgorithm.ComputeF2BCipher(request.CipherPassword);
        var user = request.Adapt<SignUpUserCommand, User>() with 
        {
            HashedPassword = password.HashedPassword,
            PasswordSalt = password.Salt,
            StatusId = _userStatuses.Pending,
            IsPasswordChangeRequired = false,
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return user.Adapt<User, SignUpUserCommand.Response>();
    }

    async Task<bool> DoesEmailAddressExists(IdentityDbContext context, string emailAddress, CancellationToken cancellationToken)
    {
        return await context.Users.AsNoTracking()
            .Where(user => user.EmailAddress == emailAddress && !user.IsDeleted)
            .AnyAsync(cancellationToken);
    }
}
