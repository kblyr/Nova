namespace Nova.Validation;

public record struct AccessValidationResult(bool IsSucceeded, IEnumerable<IAccessValidationRule> FailedRules);