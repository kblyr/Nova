namespace Nova.Identity.Handlers;

sealed class UserEmailVerifiedHandler : INotificationHandler<UserEmailVerifiedEvent>
{
    readonly IDbContextFactory<IdentityDbContext> _contextFactory;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly UserStatusesLookup _userStatuses;

    public UserEmailVerifiedHandler(IDbContextFactory<IdentityDbContext> contextFactory, ICurrentAuditInfoProvider auditInfoProvider, IOptions<UserStatusesLookup> userStatuses)
    {
        _contextFactory = contextFactory;
        _auditInfoProvider = auditInfoProvider;
        _userStatuses = userStatuses.Value;
    }

    public async Task Handle(UserEmailVerifiedEvent notification, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var user = await context.Users
            .Where(user => user.Id == notification.UserId && !user.IsDeleted)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null || user.StatusId != _userStatuses.Pending)
        {
            return;
        }

        var auditInfo = _auditInfoProvider.Current;
        user.StatusId = _userStatuses.Active;
        user.UpdatedById = auditInfo.UserId;
        user.UpdatedOn = auditInfo.Timestamp;
        await context.SaveChangesAsync();
        await transaction.CommitAsync(cancellationToken);
    }
}
