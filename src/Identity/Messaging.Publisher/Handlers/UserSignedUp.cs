namespace Nova.Identity.Handlers;

sealed class UserSignedUpHandler : INotificationHandler<UserSignedUpEvent>
{
    readonly IBusAdapter _bus;

    public UserSignedUpHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public Task Handle(UserSignedUpEvent notification, CancellationToken cancellationToken)
    {
        return _bus.Publish(notification, cancellationToken);
    }
}
