namespace Nova.Identity.Handlers;

sealed class UserAddedHandler : INotificationHandler<UserAddedEvent>
{
    readonly IBusAdapter _bus;

    public UserAddedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserAddedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
