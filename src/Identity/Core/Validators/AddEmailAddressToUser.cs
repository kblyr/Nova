using FluentValidation;

namespace Nova.Identity.Validators;

public sealed class AddEmailAddressToUserValidator : AbstractValidator<AddEmailAddressToUserCommand>
{
    public AddEmailAddressToUserValidator(IOptions<UserConfig> user)
    {
        RuleFor(_ => _.UserId)
            .NotEqual(0);

        RuleFor(_ => _.EmailAddress)
            .NotEmpty()
            .MinimumLength(user.Value.EmailAddress.MinLength)
            .MaximumLength(user.Value.EmailAddress.MaxLength);
    }
}

sealed class AddEmailAddressToUserAccessValidationConfiguration : IAccessValidationConfiguration<AddEmailAddressToUserCommand>
{
    readonly PermissionsConfig _permissions;

    public AddEmailAddressToUserAccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<AddEmailAddressToUserCommand> context)
    {
        context.RequirePermission(_permissions.AddUserEmailAddress);
    }
}
