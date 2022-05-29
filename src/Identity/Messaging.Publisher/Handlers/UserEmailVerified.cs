namespace Nova.Identity.Handlers;

sealed class UserEmailVerifiedHandler : INotificationHandler<UserEmailVerifiedEvent>
{
    readonly IBusAdapter _bus;

    public UserEmailVerifiedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public Task Handle(UserEmailVerifiedEvent notification, CancellationToken cancellationToken)
    {
        return _bus.Publish(notification, cancellationToken);
    }
}
