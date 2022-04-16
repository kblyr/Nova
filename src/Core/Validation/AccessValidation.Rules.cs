namespace Nova.Validation;

public interface IAccessValidationRule
{
    string ErrorMessage { get; }
}

public record ValidateByPermission(string Permission) : IAccessValidationRule
{
    public string ErrorMessage => $"Access validation failed. Requires Permission: {Permission}";
}

public record ValidateByRole(string Role) : IAccessValidationRule
{
    public string ErrorMessage => $"Access validation failed. Requires Role: {Role}";
}

public record ValidateAny(RequiredAccessValidationRules RequiredRules) : IAccessValidationRule
{
    public string ErrorMessage { get; } = "Access validation failed. No rules succeeded.";
}

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
