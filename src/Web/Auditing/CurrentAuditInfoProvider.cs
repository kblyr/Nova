using Microsoft.AspNetCore.Http;
using Nova.Authentication;

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

            if (context is not null)
            {
                var claim = context.User.FindFirst(ClaimTypes.User.Id);

                if (claim is not null && int.TryParse(claim.Value, out int userId))
                    _current = _current with { UserId = userId };

            }

            return _current;
        }
    }
}