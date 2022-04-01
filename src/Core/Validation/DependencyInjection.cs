using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nova.Validation;

namespace Nova.Core.Validation;

public static class DependencyExtensions
{
    public static DependencyInjector AddValidation(this DependencyInjector injector)
    {
        injector.Services
            .AddScoped<InternalAccessValidator>()
            .AddScoped<IAccessValidator, AccessValidator>()
            .AddScoped<ICurrentPermissionIdsProvider, CurrentPermissionIdsProvider>()
            .AddScoped<ICurrentRoleIdsProvider, CurrentRoleIdsProvider>()
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return injector;
    }
}