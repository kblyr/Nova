namespace Nova.Identity.Consumers;

public sealed class CreateUserEmailAddressVerificationLinkRequestedConsumer : IConsumer<CreateUserEmailAddressVerificationLinkRequestedEvent>
{
    readonly IMediator _mediator;

    public CreateUserEmailAddressVerificationLinkRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateUserEmailAddressVerificationLinkRequestedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
