using FluentValidation;

namespace Nova.Identity.Validators;

public sealed class AddUserPasswordLoginValidator : AbstractValidator<AddUserPasswordLoginCommand>
{
    public AddUserPasswordLoginValidator()
    {
        RuleFor(_ => _.UserId)
            .NotEqual(0);

        RuleFor(_ => _.SecurePassword)
            .NotEmpty();
    }
}
