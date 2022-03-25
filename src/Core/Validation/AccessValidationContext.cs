using Nova.Validation.Rules;

namespace Nova.Validation;

public interface IAccessValidationContext<T>
{
    T Data { get; }
    IEnumerable<IAccessValidationRule> Rules { get; }
    void Require(IAccessValidationRule rule);
    void Require(IAccessValidationRule rule, Predicate<T> predicate);
    void Require(Func<IAccessValidationRule> materializeRule, Predicate<T> predicate);
}

public class AccessValidationContext<T> : IAccessValidationContext<T>
{
    readonly T _data;
    readonly List<IAccessValidationRule> _rules = new();

    public T Data => _data;

    public IEnumerable<IAccessValidationRule> Rules => _rules;

    public AccessValidationContext(T data)
    {
        _data = data;
    }

    public void Require(IAccessValidationRule rule)
    {
        _rules.Add(rule);
    }

    public void Require(IAccessValidationRule rule, Predicate<T> predicate)
    {
        if (predicate(_data))
            _rules.Add(rule);
    }

    public void Require(Func<IAccessValidationRule> materializeRule, Predicate<T> predicate)
    {
        if (predicate(_data))
            _rules.Add(materializeRule());
    }
}