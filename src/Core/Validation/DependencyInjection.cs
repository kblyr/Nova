using System.Reflection;
using MediatR;
using Nova.Validation;

namespace Nova.Core.Validation;

public sealed class DependencyInjector : DependencyInjectorBase, IDependencyInjector
{
    public DependencyInjector(IDependencyInjector parent) : base(parent)
    {
    }
}

public static class DependencyExtensions
{
    public static Core.DependencyInjector WithValidation(this Core.DependencyInjector injector, InjectDependencies<DependencyInjector>? injectDependencies = null)
    {
        var _injector = new DependencyInjector(injector)
            .AddAccessValidators(AssemblyMarker.Assembly);
        injectDependencies?.Invoke(_injector);
        return injector;
    }

    static DependencyInjector InternalDefault(this DependencyInjector injector)
    {
        injector.Services
            .AddSingleton<IAccessValidator, AccessValidator>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));
        return injector;
    }

    public static DependencyInjector AddAccessValidators(this DependencyInjector injector, params Assembly[] assemblies)
    {
        injector.Services.AddAccessValidators(assemblies);
        return injector;
    }

    public static DependencyInjector AddAccessValidatorConfigurations(this DependencyInjector injector, params Assembly[] assemblies)
    {
        injector.Services.AddAccessValidationConfigurations(assemblies);
        return injector;
    }
}