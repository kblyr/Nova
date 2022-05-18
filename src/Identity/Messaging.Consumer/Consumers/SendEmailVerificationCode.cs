namespace Nova.Consumers;

public sealed class SendEmailVerificationCodeRequestedConsumer : IConsumer<SendEmailVerificationCodeRequestedEvent>
{
    readonly IMediator _mediator;

    public SendEmailVerificationCodeRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SendEmailVerificationCodeRequestedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
