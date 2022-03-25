using Nova.Validation.Rules;

namespace Nova.Validation;

public interface IAccessValidationContext
{
    IEnumerable<IAccessValidationRule> Rules { get; }
    void Require(IAccessValidationRule rule);
}

public class AccessValidationContext : IAccessValidationContext
{
    readonly List<IAccessValidationRule> _rules = new();

    public IEnumerable<IAccessValidationRule> Rules => _rules;

    public void Require(IAccessValidationRule rule)
    {
        _rules.Add(rule);
    }
}