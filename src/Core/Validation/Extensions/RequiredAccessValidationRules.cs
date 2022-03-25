using Nova.Validation.Rules;

namespace Nova.Validation;

public static class RequiredAccessValidationRules_Extensions
{
    public static RequiredAccessValidationRules RequireRoleId(this RequiredAccessValidationRules requiredRules, int roleId)
    {
        return requiredRules.Require(new ValidateByRoleId(roleId));
    }

    public static RequiredAccessValidationRules RequireRoleId(this RequiredAccessValidationRules requiredRules, int roleId, Func<bool> predicate)
    {
        return requiredRules.Require(() => new ValidateByRoleId(roleId), predicate);
    }

    public static RequiredAccessValidationRules RequirePermissionId(this RequiredAccessValidationRules requiredRules, int permissionId)
    {
        return requiredRules.Require(new ValidateByPermissionId(permissionId));
    }

    public static RequiredAccessValidationRules RequirePermissionId(this RequiredAccessValidationRules requiredRules, int permisssionId, Func<bool> predicate)
    {
        return requiredRules.Require(() => new ValidateByPermissionId(permisssionId), predicate);
    }
}