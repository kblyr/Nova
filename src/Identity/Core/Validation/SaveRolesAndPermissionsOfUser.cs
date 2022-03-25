using FluentValidation;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Validation;

sealed class SaveRolesAndPermisionsOfUser_Validator : AbstractValidator<SaveRolesAndPermissionsOfUser>
{
    public SaveRolesAndPermisionsOfUser_Validator()
    {
        RuleFor(request => request.UserId).NotEqual(0);
        RuleForEach(request => request.Roles.Added).NotEqual(0);
        RuleForEach(request => request.Roles.Removed).NotEqual(0);
        RuleForEach(request => request.Permissions.Added).NotEqual(0);
        RuleForEach(request => request.Permissions.Removed).NotEqual(0);
    }
}

sealed class SaveRolesAndPermissionsOfUser_AccessValidationConfiguration : IAccessValidationConfiguration<SaveRolesAndPermissionsOfUser>
{
    readonly PermissionsConfig _permissions;

    public SaveRolesAndPermissionsOfUser_AccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<SaveRolesAndPermissionsOfUser> context)
    {
        if (context.Data.Roles is not null)
        {
            if (context.Data.Roles.Added?.Any() ?? false)
                context.RequirePermissionId(_permissions.AddUserRole);

            if (context.Data.Roles.Removed?.Any() ?? false)
                context.RequirePermissionId(_permissions.DeleteUserRole);
        }

        if (context.Data.Permissions is not null)
        {
            if (context.Data.Permissions.Added?.Any() ?? false)
                context.RequirePermissionId(_permissions.AddUserPermission);

            if (context.Data.Permissions.Removed?.Any() ?? false)
                context.RequirePermissionId(_permissions.DeleteUserPermission);
        }
    }
}
