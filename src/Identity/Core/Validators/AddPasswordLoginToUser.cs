using FluentValidation;

namespace Nova.Identity.Validators;

public sealed class AddPasswordLoginToUserValidator : AbstractValidator<AddPasswordLoginToUserCommand>
{
    public AddPasswordLoginToUserValidator()
    {
        RuleFor(_ => _.UserId)
            .NotEqual(0);

        RuleFor(_ => _.SecurePassword)
            .NotEmpty();
    }
}

sealed class AddPasswordLoginToUserAccessValidationConfiguration : IAccessValidationConfiguration<AddPasswordLoginToUserCommand>
{
    readonly PermissionsConfig _permissions;

    public AddPasswordLoginToUserAccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<AddPasswordLoginToUserCommand> context)
    {
        context.RequirePermission(_permissions.AddUserPasswordLogin);
    }
}
