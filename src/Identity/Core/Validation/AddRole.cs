using FluentValidation;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Validation;

sealed class AddRole_Validator : AbstractValidator<AddRole>
{
    public AddRole_Validator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100);
        RuleFor(request => request.ApplicationId).NotEqual((short)0);
        RuleFor(request => request.DomainId).NotEqual((short)0);
    }
}

sealed class AddRole_AccessValidationConfiguration : IAccessValidationConfiguration<AddRole>
{
    readonly PermissionsConfig _permissions;

    public AddRole_AccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<AddRole> context)
    {
        context.RequirePermissionId(_permissions.AddRole);
    }
}
