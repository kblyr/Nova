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
