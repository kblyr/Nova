namespace Nova.Handlers;

sealed class SendEmailVerificationCodeRequestedHandler : INotificationHandler<SendEmailVerificationCodeRequestedEvent>
{
    readonly IBusAdapter _bus;

    public SendEmailVerificationCodeRequestedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(SendEmailVerificationCodeRequestedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
