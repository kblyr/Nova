namespace Nova;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection WithPayloads(this IServiceCollection services, Action<PayloadServiceConfigurator> configurePayloads)
    {
        var configurator = new PayloadServiceConfigurator(services);
        configurePayloads.Invoke(configurator);
        return services;
    }
}

public class PayloadServiceConfigurator
{
    public IServiceCollection Services { get; }

    internal PayloadServiceConfigurator(IServiceCollection services)
    {
        Services = services;
    }

    public PayloadServiceConfigurator<T> For<T>() => new(this);
}

public class PayloadServiceConfigurator<T> : PayloadServiceConfigurator
{
    internal PayloadServiceConfigurator(IServiceCollection services) : base(services)
    {
    }

    internal PayloadServiceConfigurator(PayloadServiceConfigurator parent) : this(parent.Services)
    {
    }
}