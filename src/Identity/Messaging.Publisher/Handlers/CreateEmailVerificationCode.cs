namespace Nova.Handlers;

sealed class CreateEmailVerificationCodeRequestedHandler : INotificationHandler<CreateEmailVerificationCodeRequestedEvent>
{
    readonly IBusAdapter _bus;

    public CreateEmailVerificationCodeRequestedHandler(IBusAdapter bus)
    {
        _bus = bus;
    }

    public async Task Handle(CreateEmailVerificationCodeRequestedEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification, cancellationToken);
    }
}
