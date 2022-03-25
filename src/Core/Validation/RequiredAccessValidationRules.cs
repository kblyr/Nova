using Nova.Validation.Rules;

namespace Nova.Validation;

public class RequiredAccessValidationRules
{
    readonly List<IAccessValidationRule> _rules = new();

    public IEnumerable<IAccessValidationRule> Rules => _rules;

    public RequiredAccessValidationRules Require(IAccessValidationRule rule)
    {
        _rules.Add(rule);
        return this;
    }

    public RequiredAccessValidationRules Require(Func<IAccessValidationRule> materializeRule, Func<bool> predicate)
    {
        if (predicate())
            _rules.Add(materializeRule());

        return this;
    }
}