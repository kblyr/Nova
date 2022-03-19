using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Nova.Authentication.ClaimTypes;

namespace Nova.Auditing;

sealed class CurrentAuditInfoProvider : ICurrentAuditInfoProvider
{
    readonly IHttpContextAccessor _contextAccessor;

    public CurrentAuditInfoProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    AuditInfo? _current;

    public AuditInfo Current
    {
        get
        {
            if (_current is not null)
                return _current;

            _current = new(null, DateTimeOffset.UtcNow);
            var context = _contextAccessor.HttpContext;

            if (context is null)
                return _current;

            var claim = context.User.FindFirst(SessionClaimType.ClaimTypeName);

            if (claim is null)
                return _current;
            
            var session = JsonSerializer.Deserialize<SessionClaimType>(claim.Value);

            if (session is null || session.User is null)
                return _current;

            _current = _current with { UserId = session.User.Id };

            return _current;
        }
    }
}