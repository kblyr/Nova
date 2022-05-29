namespace Nova.Identity.Handlers;

sealed class EmailVerificationCodeSentHandler : INotificationHandler<EmailVerificationCodeSentEvent>
{
    readonly IBusAdapter _bus;

    public EmailVerificationCodeSentHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(EmailVerificationCodeSentEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
