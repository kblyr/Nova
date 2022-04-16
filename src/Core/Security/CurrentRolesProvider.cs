namespace Nova.Security;

public interface ICurrentRolesProvider
{
    IEnumerable<string> Roles { get; }
}

sealed class CurrentRolesProvider : ICurrentRolesProvider
{
    readonly ICurrentSessionProvider _sessionProvider;

    public CurrentRolesProvider(ICurrentSessionProvider sessionProvider)
    {
        _sessionProvider = sessionProvider;
    }

    IEnumerable<string>? _roles;
    public IEnumerable<string> Roles
    {
        get
        {
            if (_roles is not null)
                return _roles;

            var session = _sessionProvider.Current;
            _roles = session.Roles;
            return _roles;
        }
    }
}