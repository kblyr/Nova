using FluentValidation;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Validation;

sealed class AddUser_Validator : AbstractValidator<AddUser>
{
    public AddUser_Validator()
    {
        RuleFor(request => request.Username)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100);
        RuleFor(request => request.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100);
    }
}

sealed class AddUser_AccessValidationConfiguration : IAccessValidationConfiguration<AddUser>
{
    readonly PermissionsConfig _permissions;

    public AddUser_AccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<AddUser> context)
    {
        context.RequirePermissionId(_permissions.AddUser);
    }
}
