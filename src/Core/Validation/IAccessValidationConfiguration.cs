namespace Nova.Validation;

public interface IAccessValidationConfiguration<T> where T : notnull
{
    void Configure(IAccessValidationContext<T> context);
}