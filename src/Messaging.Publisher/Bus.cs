namespace Nova;

public interface IBusAdapter
{
    Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class;
}

sealed class BusAdapter : IBusAdapter
{
    readonly IBusControl _bus;
    readonly IPublishFailureHandler _failureHandler;

    public BusAdapter(IBusControl bus, IPublishFailureHandler failureHandler)
    {
        _bus = bus;
        _failureHandler = failureHandler;
    }

    public async Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            await _bus.Publish(message, cancellationToken);
        }
        catch (Exception ex)
        {
            await _failureHandler.Handle(message, ex);
        }
    }
}