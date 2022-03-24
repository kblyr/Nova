using Nova.Validation.Rules;

namespace Nova.Validation;

sealed class AnyAccessValidator : IAccessValidator<ValidateAny>
{
    readonly InternalAccessValidator _internalValidator;

    public AnyAccessValidator(InternalAccessValidator internalValidator)
    {
        _internalValidator = internalValidator;
    }

    public bool Validate(ValidateAny rule)
    {
        if (rule.Rules is null || !rule.Rules.Any())
            return true;

        foreach (var _rule in rule.Rules)
        {
            if (_internalValidator.Validate(_rule))
                return true;
        }

        return false;
    }
}
