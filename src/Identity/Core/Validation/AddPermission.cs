using FluentValidation;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Validation;

sealed class AddPermission_Validator : AbstractValidator<AddPermission>
{
    public AddPermission_Validator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100);
        RuleFor(request => request.ApplicationId).NotEqual((short)0);
        RuleFor(request => request.DomainId).NotEqual((short)0);
    }
}

sealed class AddPermission_AccessValidationConfiguration : IAccessValidationConfiguration<AddPermission>
{
    readonly PermissionsConfig _permissions;

    public AddPermission_AccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<AddPermission> context)
    {
        context.RequirePermissionId(_permissions.AddPermission); 
    }
}
