using Nova.Authentication;

namespace Nova.Auditing;

sealed class CurrentAuditInfoProvider : ICurrentAuditInfoProvider
{
    readonly ICurrentSessionProvider _sessionProvider;

    public CurrentAuditInfoProvider(ICurrentSessionProvider sessionProvider)
    {
        _sessionProvider = sessionProvider;
    }

    AuditInfo? _current;

    public AuditInfo Current
    {
        get
        {
            if (_current is not null)
                return _current;

            _current = new(null, DateTimeOffset.UtcNow);
            var session = _sessionProvider.Current;

            if (session is not null)
            {
                _current = _current with { UserId = session.UserId };
            }

            return _current;
        }
    }
}