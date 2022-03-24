namespace Nova.Validation.Rules;

public record ValidateAny(IEnumerable<IAccessValidationRule> Rules) : IAccessValidationRule
{
    public string ErrorMessage { get; } = "Access validation failed. No rules succeeded.";
}