using Nova.Validation.Rules;

namespace Nova.Validation;

sealed class RoleIdAccessValidator : IAccessValidator<ValidateByRoleId>
{
    readonly ICurrentRoleIdsProvider _roleIdsProvider;

    public RoleIdAccessValidator(ICurrentRoleIdsProvider roleIdsProvider)
    {
        _roleIdsProvider = roleIdsProvider;
    }

    public bool Validate(ValidateByRoleId rule)
    {
        var roleIds = _roleIdsProvider.RoleIds;
        return roleIds is not null && roleIds.Any(id => id == rule.RoleId); 
    }
}
