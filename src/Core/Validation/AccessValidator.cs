namespace Nova.Validation;

public interface IAccessValidator
{
    Task<AccessValidationResult> Validate(IEnumerable<IAccessValidationRule> rules);
}

public record struct AccessValidationResult(bool Status, IEnumerable<IAccessValidationRule> FailedRules);

public interface IAccessValidationRule { }