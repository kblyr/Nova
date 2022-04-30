namespace Nova.Identity.Consumers;

sealed class UserEmailVerificationSendRequestedConsumer : IConsumer<UserEmailVerificationSendRequestedEvent>
{
    readonly IMediator _mediator;

    public UserEmailVerificationSendRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserEmailVerificationSendRequestedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
