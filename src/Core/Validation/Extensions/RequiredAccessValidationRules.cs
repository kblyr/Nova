namespace Nova.Validation;

public static class RequiredAccessValidationRulesExtensions
{
    public static RequiredAccessValidationRules RequireRole(this RequiredAccessValidationRules requiredRules, string role)
    {
        return requiredRules.Require(new ValidateByRole(role));
    }

    public static RequiredAccessValidationRules RequireRole(this RequiredAccessValidationRules requiredRules, string role, Func<bool> predicate)
    {
        return requiredRules.Require(() => new ValidateByRole(role), predicate);
    }

    public static RequiredAccessValidationRules RequirePermission(this RequiredAccessValidationRules requiredRules, string permission)
    {
        return requiredRules.Require(new ValidateByPermission(permission));
    }

    public static RequiredAccessValidationRules RequirePermission(this RequiredAccessValidationRules requiredRules, string permisssion, Func<bool> predicate)
    {
        return requiredRules.Require(() => new ValidateByPermission(permisssion), predicate);
    }
}