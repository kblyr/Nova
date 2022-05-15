namespace Nova.Identity.Consumers;

public sealed class UserEmailAddressVerifiedConsumer : IConsumer<UserEmailAddressVerifiedEvent>
{
    readonly IMediator _mediator;

    public UserEmailAddressVerifiedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserEmailAddressVerifiedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
