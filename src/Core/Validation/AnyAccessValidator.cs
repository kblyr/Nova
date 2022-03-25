using Nova.Validation.Rules;

namespace Nova.Validation;

sealed class AnyAccessValidator<T> : IAccessValidator<ValidateAny>
{
    readonly InternalAccessValidator _internalValidator;

    public AnyAccessValidator(InternalAccessValidator internalValidator)
    {
        _internalValidator = internalValidator;
    }

    public bool Validate(ValidateAny rule)
    {
        if (rule.RequiredRules is null || !rule.RequiredRules.Rules.Any())
            return true;

        foreach (var _rule in rule.RequiredRules.Rules)
        {
            if (_internalValidator.Validate(_rule))
                return true;
        }

        return false;
    }
}
