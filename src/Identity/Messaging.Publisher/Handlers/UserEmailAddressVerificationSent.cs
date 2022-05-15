namespace Nova.Identity.Handlers;

sealed class UserEmailAddressVerificationSentHandler : INotificationHandler<UserEmailAddressVerificationSentEvent>
{
    readonly IBusAdapter _bus;

    public UserEmailAddressVerificationSentHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserEmailAddressVerificationSentEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
