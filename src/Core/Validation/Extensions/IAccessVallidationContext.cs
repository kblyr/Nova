using Nova.Validation.Rules;

namespace Nova.Validation;

public static class IAccessValidationContext_Extensions
{
    public static IAccessValidationContext<T> RequireRoleId<T>(this IAccessValidationContext<T> context, int roleId)
    {
        context.Require(new ValidateByRoleId(roleId));
        return context;
    }

    public static IAccessValidationContext<T> RequireRoleId<T>(this IAccessValidationContext<T> context, int roleId, Predicate<T> predicate)
    {
        context.Require(() => new ValidateByRoleId(roleId), predicate);
        return context;
    }

    public static IAccessValidationContext<T> RequirePermissionId<T>(this IAccessValidationContext<T> context, int permissionId)
    {
        context.Require(new ValidateByPermissionId(permissionId));
        return context;
    }

    public static IAccessValidationContext<T> RequirePermissionId<T>(this IAccessValidationContext<T> context, int permissionId, Predicate<T> predicate)
    {
        context.Require(() => new ValidateByPermissionId(permissionId), predicate);
        return context;
    }

    public static IAccessValidationContext<T> RequireAny<T>(this IAccessValidationContext<T> context, Action<RequiredAccessValidationRules> addRequiredRules)
    {
        var requiredRules = new RequiredAccessValidationRules();
        addRequiredRules(requiredRules);
        context.Require(new ValidateAny(requiredRules));
        return context;
    }
}
