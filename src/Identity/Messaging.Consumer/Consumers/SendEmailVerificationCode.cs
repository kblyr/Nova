namespace Nova.Identity.Consumers;

public sealed class SendEmailVerificationCodeRequestedConsumer : IConsumer<SendEmailVerificationCodeRequestedEvent>
{
    readonly IMediator _mediator;

    public SendEmailVerificationCodeRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SendEmailVerificationCodeRequestedEvent> context)
    {
        var response = await _mediator.Send(context.Message.Adapt<SendEmailVerificationCodeRequestedEvent, SendEmailVerificationCodeCommand>());
    }
}