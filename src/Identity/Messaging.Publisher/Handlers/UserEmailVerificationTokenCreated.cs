namespace Nova.Identity.Handlers;

sealed class UserEmailVerificationTokenCreatedHandler : INotificationHandler<UserEmailVerificationTokenCreatedEvent>
{
    readonly IBus _bus;

    public UserEmailVerificationTokenCreatedHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserEmailVerificationTokenCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);
    }
}
