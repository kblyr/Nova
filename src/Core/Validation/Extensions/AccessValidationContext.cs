namespace Nova.Validation;

public static class IAccessValidationContextExtensions
{
    public static IAccessValidationContext<T> RequireRole<T>(this IAccessValidationContext<T> context, string role)
    {
        context.Require(new ValidateByRole(role));
        return context;
    }

    public static IAccessValidationContext<T> RequireRole<T>(this IAccessValidationContext<T> context, string role, Predicate<T> predicate)
    {
        context.Require(() => new ValidateByRole(role), predicate);
        return context;
    }

    public static IAccessValidationContext<T> RequirePermission<T>(this IAccessValidationContext<T> context, string permission)
    {
        context.Require(new ValidateByPermission(permission));
        return context;
    }

    public static IAccessValidationContext<T> RequirePermission<T>(this IAccessValidationContext<T> context, string permission, Predicate<T> predicate)
    {
        context.Require(() => new ValidateByPermission(permission), predicate);
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