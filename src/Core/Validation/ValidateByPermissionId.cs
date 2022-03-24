namespace Nova.Validation.Rules;

public record ValidateByPermissionId(int PermissionId) : IAccessValidationRule
{
    public string ErrorMessage => $"Access validation failed. Requires Permission ID: {PermissionId}";
}