using System.Reflection.Emit;
using Nova.Validation.Rules;

namespace Nova.Validation;

public static class IAccessValidationContext_Extensions
{
    public static IAccessValidationContext RequireRoleId(this IAccessValidationContext context, int roleId)
    {
        context.Require(new ValidateByRoleId(roleId));
        return context;
    }

    public static IAccessValidationContext RequirePermissionId(this IAccessValidationContext context, int permissionId)
    {
        context.Require(new ValidateByPermissionId(permissionId));
        return context;
    }

    public static IAccessValidationContext RequireAny(this IAccessValidationContext context, Action<RequiredAccessValidationRules> addRequiredRules)
    {
        var requiredRules = new RequiredAccessValidationRules();
        addRequiredRules(requiredRules);
        context.Require(new ValidateAny(requiredRules));
        return context;
    }
}
