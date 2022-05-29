namespace Nova.Auditing;

sealed class CurrentAuditInfoProvider : ICurrentAuditInfoProvider
{
    readonly IHttpContextAccessor _contextAccessor;

    public CurrentAuditInfoProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    AuditInfo? _current;
    public AuditInfo Current => _current ??= GetCurrent();

    AuditInfo GetCurrent()
    {
        int? userId = null;

        return new AuditInfo
        {
            UserId = userId,
            Timestamp = DateTimeOffset.UtcNow
        };
    }
}
