namespace Nova.Identity.Consumers;

public sealed class SendUserCreationConfirmationMailRequestedConsumer : IConsumer<SendUserCreationConfirmationMailRequestedEvent>
{
    readonly IMediator _mediator;

    public SendUserCreationConfirmationMailRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SendUserCreationConfirmationMailRequestedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
