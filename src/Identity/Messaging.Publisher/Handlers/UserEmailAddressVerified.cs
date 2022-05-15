namespace Nova.Identity.Handlers;

sealed class UserEmailAddressVerifiedHandler : INotificationHandler<UserEmailAddressVerifiedEvent>
{
    readonly IBusAdapter _bus;

    public UserEmailAddressVerifiedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserEmailAddressVerifiedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
