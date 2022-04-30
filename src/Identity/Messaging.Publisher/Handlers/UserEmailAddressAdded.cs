namespace Nova.Identity.Handlers;

sealed class UserEmailAddressAddedHandler : INotificationHandler<UserEmailAddressAddedEvent>
{
    readonly IBus _bus;

    public UserEmailAddressAddedHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserEmailAddressAddedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);
    }
}
