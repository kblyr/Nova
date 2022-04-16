namespace Nova.Security;

public interface ICurrentPermissionsProvider
{
    IEnumerable<string> Permissions { get; }
}

sealed class CurrentPermissionsProvider : ICurrentPermissionsProvider
{
    readonly ICurrentSessionProvider _sessionProvider;

    public CurrentPermissionsProvider(ICurrentSessionProvider sessionProvider)
    {
        _sessionProvider = sessionProvider;
    }

    IEnumerable<string>? _permissions;
    public IEnumerable<string> Permissions
    {
        get
        {
            if (_permissions is not null)
                return _permissions;

            var session = _sessionProvider.Current;
            _permissions = session.Permissions;
            return _permissions;
        }
    }
}