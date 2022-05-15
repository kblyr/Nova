namespace Nova.Identity.Consumers;

public sealed class CreateUserEmailAddressVerificationTokenRequestedConsumer : IConsumer<CreateUserEmailAddressVerificationTokenRequestedEvent>
{
    readonly IMediator _mediator;

    public CreateUserEmailAddressVerificationTokenRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateUserEmailAddressVerificationTokenRequestedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
