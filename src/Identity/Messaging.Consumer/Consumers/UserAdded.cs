namespace Nova.Identity.Consumers;

public sealed class UserAddedConsumer : IConsumer<UserAddedEvent>
{
    readonly IMediator _mediator;

    public UserAddedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserAddedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
