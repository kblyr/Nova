namespace Nova.Validation.Rules;

public record ValidateByRoleId(int RoleId) : IAccessValidationRule
{
    public string ErrorMessage => $"Access validation failed. Requires Role ID: {RoleId}";
}
