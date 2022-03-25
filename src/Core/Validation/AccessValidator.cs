using System.Collections.Concurrent;
using MediatR;
using Nova.Validation.Rules;

namespace Nova.Validation;

public interface IAccessValidator
{
    AccessValidationResult Validate(IEnumerable<IAccessValidationRule> rules);
}

public abstract class AccessValidatorBase
{
    protected abstract bool Validate(IAccessValidationRule rule);

    public AccessValidationResult Validate(IEnumerable<IAccessValidationRule> rules)
    {
        if (rules is null || !rules.Any())
            return new(true, Enumerable.Empty<IAccessValidationRule>());

        foreach (var rule in rules)
        {
            if (Validate(rule))
                return new(false, new[] { rule });
        }

        return new(true, Enumerable.Empty<IAccessValidationRule>());
    }
}

sealed class AccessValidator : AccessValidatorBase, IAccessValidator
{
    readonly InternalAccessValidator _internal;

    public AccessValidator(InternalAccessValidator internalValidator)
    {
        _internal = internalValidator;
    }

    protected override bool Validate(IAccessValidationRule rule)
    {
        return _internal.Validate(rule);
    }
}

sealed class InternalAccessValidator
{
    static readonly ConcurrentDictionary<Type, AccessValidatorWrapperBase> _accessValidatorImpls = new();

    readonly ServiceFactory _serviceFactory;

    public InternalAccessValidator(ServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public bool Validate(IAccessValidationRule rule)
    {
        var ruleType = rule.GetType();

        var _accessValidatorImpl = _accessValidatorImpls.GetOrAdd(ruleType,
            type => (AccessValidatorWrapperBase?)(Activator.CreateInstance(typeof(AccessValidatorWrapperImpl<>).MakeGenericType(ruleType)))
            ?? throw new Exception($"Could not create wrapper for type '{ruleType.FullName}'")
        );

        return _accessValidatorImpl.Validate(rule, _serviceFactory);
    }
}

public interface IAccessValidator<TRule> where TRule : IAccessValidationRule
{
    bool Validate(TRule rule);
}

abstract class AccessValidatorWrapperBase 
{
    public abstract bool Validate(object rule, ServiceFactory serviceFactory);

    protected static T GetImpl<T>(ServiceFactory serviceFactory)
    {
        return serviceFactory.GetInstance<T>() ?? throw new Exception($"Cannot find implementation for type '{typeof(T).FullName}'");
    }
}

abstract class AccessValidatorWrapper<TRule> : AccessValidatorWrapperBase where TRule : IAccessValidationRule
{
    public abstract bool Validate(TRule rule, ServiceFactory serviceFactory);
}


sealed class AccessValidatorWrapperImpl<TRule> : AccessValidatorWrapper<TRule> where TRule : IAccessValidationRule
{
    public override bool Validate(TRule rule, ServiceFactory serviceFactory)
    {
        return GetImpl<IAccessValidator<TRule>>(serviceFactory).Validate(rule);
    }

    public override bool Validate(object rule, ServiceFactory serviceFactory)
    {
        return Validate((TRule)rule, serviceFactory);
    }
}