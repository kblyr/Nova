namespace Nova.Identity.Consumers;

public sealed class SendUserEmailAddressVerificationRequestedConsumer : IConsumer<SendUserEmailAddressVerificationRequestedEvent>
{
    readonly IMediator _mediator;

    public SendUserEmailAddressVerificationRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SendUserEmailAddressVerificationRequestedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
