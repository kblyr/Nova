using FluentValidation;

namespace Nova.Identity.Validators;

public sealed class AddUserEmailAddressValidator : AbstractValidator<AddUserEmailAddressCommand>
{
    public AddUserEmailAddressValidator(IOptions<UserConfig> user)
    {
        RuleFor(_ => _.UserId)
            .NotEqual(0);

        RuleFor(_ => _.EmailAddress)
            .NotEmpty()
            .MinimumLength(user.Value.EmailAddress.MinLength)
            .MaximumLength(user.Value.EmailAddress.MaxLength);
    }
}