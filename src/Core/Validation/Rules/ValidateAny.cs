namespace Nova.Validation.Rules;

public record ValidateAny(RequiredAccessValidationRules RequiredRules) : IAccessValidationRule
{
    public string ErrorMessage { get; } = "Access validation failed. No rules succeeded.";
}