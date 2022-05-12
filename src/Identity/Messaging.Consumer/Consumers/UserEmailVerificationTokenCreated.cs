namespace Nova.Identity.Consumers;

sealed class UserEmailVerificationTokenCreatedConsumer : IConsumer<UserEmailVerificationTokenCreatedEvent>
{
    readonly IMediator _mediator;

    public UserEmailVerificationTokenCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserEmailVerificationTokenCreatedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
