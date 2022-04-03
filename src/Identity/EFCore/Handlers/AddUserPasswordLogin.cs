using Nova.Identity.Security;
using Nova.Security;

namespace Nova.Identity.Handlers;

sealed class AddUserPasswordLoginHandler : IRequestHandler<AddUserPasswordLoginCommand>
{
    readonly IDbContextFactory<UserPasswordLoginDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly IStringDecryptor _decryptor;
    readonly IUserPasswordHashComputer _passwordHashComputer;

    public AddUserPasswordLoginHandler(IDbContextFactory<UserPasswordLoginDbContext> contextFactory, ICurrentAuditInfoProvider auditInfoProvider, IStringDecryptor decryptor, IUserPasswordHashComputer passwordHashComputer)
    {
        _contextFactory = contextFactory;
        _auditInfoProvider = auditInfoProvider;
        _decryptor = decryptor;
        _passwordHashComputer = passwordHashComputer;
    }

    public async Task<IResponse> Handle(AddUserPasswordLoginCommand request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        if (await context.Users.Exists(request.UserId))
            return new UserNotFoundResponse(request.UserId);

        if (await context.UserPasswordLogins.Exists(request.UserId))
            return new UserPasswordLoginAlreadyExistsResponse(request.UserId);

        var auditInfo = _auditInfoProvider.Current;
        var login = new UserPasswordLogin
        {
            UserId = request.UserId,
            HashedPassword = _passwordHashComputer.Compute(_decryptor.Decrypt(request.SecurePassword)),
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.UserPasswordLogins.Add(login);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        return new AddUserPasswordLoginCommand.Response(login.Id);
    }
}
