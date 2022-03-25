using FluentValidation;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Validation;

sealed class SavePermissionsOfRole_Validator : AbstractValidator<SavePermissionsOfRole>
{
    public SavePermissionsOfRole_Validator()
    {
        RuleFor(request => request.RoleId).NotEqual(0);
        RuleForEach(request => request.AddedIds).NotEqual(0);
        RuleForEach(request => request.RemovedIds).NotEqual(0);
    }
}

sealed class SavePermissionsOfRole_AccessValidationConfiguration : IAccessValidationConfiguration<SavePermissionsOfRole>
{
    readonly PermissionsConfig _permissions;

    public SavePermissionsOfRole_AccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<SavePermissionsOfRole> context)
    {
        context.RequirePermissionId(_permissions.AddRolePermission, data => data.AddedIds?.Any() ?? false);
        context.RequirePermissionId(_permissions.DeleteRolePermission, data => data.RemovedIds?.Any() ?? false);
    }
}