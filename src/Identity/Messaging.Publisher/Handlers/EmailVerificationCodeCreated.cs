namespace Nova.Identity.Handlers;

sealed class EmailVerificationCodeCreatedHandler : INotificationHandler<EmailVerificationCodeCreatedEvent>
{
    readonly IBusAdapter _bus;

    public EmailVerificationCodeCreatedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(EmailVerificationCodeCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
