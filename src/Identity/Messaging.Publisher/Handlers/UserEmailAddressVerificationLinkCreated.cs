namespace Nova.Identity.Handlers;

sealed class UserEmailAddressVerificationLinkCreatedHandler : INotificationHandler<UserEmailAddressVerificationLinkCreatedEvent>
{
    readonly IBusAdapter _bus;

    public UserEmailAddressVerificationLinkCreatedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserEmailAddressVerificationLinkCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
