using Nova.Security;

namespace Nova.Validation;

public interface IAccessValidator<TRule> where TRule : IAccessValidationRule
{
    bool Validate(TRule rule);
}

sealed class RoleAccessValidator : IAccessValidator<ValidateByRole>
{
    readonly ICurrentRolesProvider _rolesProvider;

    public RoleAccessValidator(ICurrentRolesProvider rolesProvider)
    {
        _rolesProvider = rolesProvider;
    }

    public bool Validate(ValidateByRole rule)
    {
        return _rolesProvider.Roles.Any(role => role == rule.Role);
    }
}

sealed class PermissionAccessValidator : IAccessValidator<ValidateByPermission>
{
    readonly ICurrentPermissionsProvider _permissionsProvider;

    public PermissionAccessValidator(ICurrentPermissionsProvider permissionsProvider)
    {
        _permissionsProvider = permissionsProvider;
    }

    public bool Validate(ValidateByPermission rule)
    {
        return _permissionsProvider.Permissions.Any(permission => permission == rule.Permission);
    }
}