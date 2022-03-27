using Nova.Authentication;

namespace Nova.Validation;

public interface ICurrentPermissionIdsProvider
{
    IEnumerable<int> PermissionIds { get; }
}

sealed class CurrentPermissionIdsProvider : ICurrentPermissionIdsProvider
{
    readonly ICurrentSessionProvider _sessionProvider;

    public CurrentPermissionIdsProvider(ICurrentSessionProvider sessionProvider)
    {
        _sessionProvider = sessionProvider;
    }

    IEnumerable<int>? _permissionIds;
    public IEnumerable<int> PermissionIds
    {
        get
        {
            if (_permissionIds is not null)
                return _permissionIds;

            var session = _sessionProvider.Current;
            _permissionIds = session.Permissions;
            return _permissionIds;
        }
    }
}