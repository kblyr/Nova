namespace Nova.Identity.Consumers;

public sealed class SendUserEmailVerificationCodeRequestedConsumer : IConsumer<SendUserEmailVerificationCodeRequestedEvent>
{
    readonly IMediator _mediator;

    public SendUserEmailVerificationCodeRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SendUserEmailVerificationCodeRequestedEvent> context)
    {
        await _mediator.Send(context.Message.Adapt<SendUserEmailVerificationCodeRequestedEvent, SendUserEmailVerificationCodeCommand>());
    }
}
