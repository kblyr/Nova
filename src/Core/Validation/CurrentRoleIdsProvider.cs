using Nova.Authentication;

namespace Nova.Validation;

public interface ICurrentRoleIdsProvider
{
    IEnumerable<int> RoleIds { get; }
}

sealed class CurrentRoleIdsProvider : ICurrentRoleIdsProvider
{
    readonly ICurrentSessionProvider _sessionProvider;

    public CurrentRoleIdsProvider(ICurrentSessionProvider sessionProvider)
    {
        _sessionProvider = sessionProvider;
    }

    IEnumerable<int>? _roleIds;
    public IEnumerable<int> RoleIds
    {
        get
        {
            if (_roleIds is not null)
                return _roleIds;

            var session = _sessionProvider.Current;
            _roleIds = session.Roles;
            return _roleIds;
        }
    }
}