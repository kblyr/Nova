using Nova.Validation.Rules;

namespace Nova.Validation;

sealed class PermissionIdAccessValidator : IAccessValidator<ValidateByPermissionId>
{
    readonly ICurrentPermissionIdsProvider _permissionIdsProvider;

    public PermissionIdAccessValidator(ICurrentPermissionIdsProvider permissionIdsProvider)
    {
        _permissionIdsProvider = permissionIdsProvider;
    }

    public bool Validate(ValidateByPermissionId rule)
    {
        var permissionIds = _permissionIdsProvider.PermissionIds;
        return permissionIds is not null && permissionIds.Any(id => id == rule.PermissionId);
    }
}
