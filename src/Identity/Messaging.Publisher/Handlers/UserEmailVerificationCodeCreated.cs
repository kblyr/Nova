namespace Nova.Identity.Handlers;

sealed class UserEmailVerificationCodeCreatedHandler : INotificationHandler<UserEmailVerificationCodeCreatedEvent>
{
    readonly IBusAdapter _bus;

    public UserEmailVerificationCodeCreatedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public Task Handle(UserEmailVerificationCodeCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _bus.Publish(notification, cancellationToken);
    }
}
