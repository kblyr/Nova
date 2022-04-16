using FluentValidation;

namespace Nova.Identity.Validators;

public sealed class AddUserValidator : AbstractValidator<AddUserCommand>
{
    public AddUserValidator(IOptions<UserConfig> user)
    {
        RuleFor(_ => _.Username)
            .NotEmpty()
            .MinimumLength(user.Value.Username.MinLength)
            .MaximumLength(user.Value.Username.MaxLength);

        RuleFor(_ => _.EmailAddress)
            .NotEmpty()
            .MinimumLength(user.Value.EmailAddress.MinLength)
            .MaximumLength(user.Value.EmailAddress.MaxLength);

        RuleFor(_ => _.StatusId)
            .NotEqual((short)0);
    }
}

sealed class AddUserAccessValidationConfiguration : IAccessValidationConfiguration<AddUserCommand>
{
    readonly PermissionsConfig _permissions;

    public AddUserAccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<AddUserCommand> context)
    {
        context.RequirePermission(_permissions.AddUser);
    }
}
