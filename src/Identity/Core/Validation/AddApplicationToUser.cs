using FluentValidation;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Validation;

sealed class AddApplicationToUser_Validator : AbstractValidator<AddApplicationToUser>
{
    public AddApplicationToUser_Validator()
    {
        RuleFor(request => request.UserId).NotEqual(0);
        RuleFor(request => request.ApplicationId).NotEqual((short)0);
    }
}

sealed class AddApplicationToUser_AccessValidationConfiguration : IAccessValidationConfiguration<AddApplicationToUser>
{
    readonly PermissionsConfig _permissions;

    public AddApplicationToUser_AccessValidationConfiguration(IOptions<PermissionsConfig> permissions)
    {
        _permissions = permissions.Value;
    }

    public void Configure(IAccessValidationContext<AddApplicationToUser> context)
    {
        context.RequirePermissionId(_permissions.AddUserApplication);
    }
}
