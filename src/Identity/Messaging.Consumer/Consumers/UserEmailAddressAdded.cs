namespace Nova.Identity.Consumers;

sealed class UserEmailAddressAddedConsumer : IConsumer<UserEmailAddressAddedEvent>
{
    readonly IMediator _mediator;

    public UserEmailAddressAddedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserEmailAddressAddedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
