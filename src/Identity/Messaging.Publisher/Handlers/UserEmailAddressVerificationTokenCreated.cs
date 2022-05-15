namespace Nova.Identity.Handlers;

sealed class UserEmailAddressVerificationTokenCreatedHandler : INotificationHandler<UserEmailAddressVerificationTokenCreatedEvent>
{
    readonly IBusAdapter _bus;

    public UserEmailAddressVerificationTokenCreatedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserEmailAddressVerificationTokenCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
