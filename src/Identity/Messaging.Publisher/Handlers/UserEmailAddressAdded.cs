namespace Nova.Identity.Handlers;

sealed class UserEmailAddressAddedHandler : INotificationHandler<UserEmailAddressAddedEvent>
{
    readonly IBusAdapter _bus;

    public UserEmailAddressAddedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserEmailAddressAddedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
