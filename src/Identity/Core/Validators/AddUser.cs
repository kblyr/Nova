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