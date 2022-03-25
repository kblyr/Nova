using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Nova.Authentication;

sealed class CurrentSessionProvider : ICurrentSessionProvider
{
    readonly IHttpContextAccessor _contextAccessor;

    public CurrentSessionProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    Session? _current;
    public Session Current
    {
        get
        {
            if (_current is not null)
                return _current;

            var context = _contextAccessor.HttpContext;

            if (context is null)
                throw new Exception($"HTTP Context is unavailable");

            var claim = context.User.FindFirst(Session.ClaimType);

            if (claim is null)
                throw new Exception("Session is unavailable");
            
            _current = JsonSerializer.Deserialize<Session>(claim.Value);
            return _current ?? throw new Exception("Session is unavailable");
        }
    }
}