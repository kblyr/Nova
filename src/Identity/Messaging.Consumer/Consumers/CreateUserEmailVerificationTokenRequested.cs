namespace Nova.Identity.Consumers;

sealed class CreateUserEmailVerificationTokenRequestedConsumer : IConsumer<CreateUserEmailVerificationTokenRequestedEvent>
{
    readonly IMediator _mediator;

    public CreateUserEmailVerificationTokenRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateUserEmailVerificationTokenRequestedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
