namespace Nova.Identity.Handlers;

sealed class UserEmailVerificationCodeSentHandler : INotificationHandler<UserEmailVerificationCodeSentEvent>
{
    readonly IBusAdapter _bus;

    public UserEmailVerificationCodeSentHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public Task Handle(UserEmailVerificationCodeSentEvent notification, CancellationToken cancellationToken)
    {
        return _bus.Publish(notification, cancellationToken);
    }
}
