namespace Nova.Identity.Consumers;

public sealed class CreateUserEmailVerificationCodeRequestedConsumer : IConsumer<CreateUserEmailVerificationCodeRequestedEvent>
{
    readonly IMediator _mediator;

    public CreateUserEmailVerificationCodeRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateUserEmailVerificationCodeRequestedEvent> context)
    {
        await _mediator.Send(context.Message.Adapt<CreateUserEmailVerificationCodeRequestedEvent, CreateUserEmailVerificationCodeCommand>());
    }
}
