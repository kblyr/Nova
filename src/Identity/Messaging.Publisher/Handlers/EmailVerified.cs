namespace Nova.Identity.Handlers;

sealed class EmailVerifiedHandler : INotificationHandler<EmailVerifiedEvent>
{
    readonly IBusAdapter _bus;

    public EmailVerifiedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(EmailVerifiedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
