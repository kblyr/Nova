namespace Nova.Identity.Consumers;

public sealed class CreateEmailVerificationCodeRequestedConsumer : IConsumer<CreateEmailVerificationCodeRequestedEvent>
{
    readonly IMediator _mediator;

    public CreateEmailVerificationCodeRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateEmailVerificationCodeRequestedEvent> context)
    {
        await _mediator.Send(context.Message.Adapt<CreateEmailVerificationCodeRequestedEvent, CreateEmailVerificationCodeCommand>());
    }
}