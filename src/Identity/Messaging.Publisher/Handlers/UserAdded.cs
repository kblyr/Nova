namespace Nova.Identity.Handlers;

sealed class UserAddedHandler : INotificationHandler<UserAddedEvent>
{
    readonly IBus _bus;

    public UserAddedHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserAddedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);
    }
}
