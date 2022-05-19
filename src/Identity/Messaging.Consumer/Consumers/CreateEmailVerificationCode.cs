namespace Nova.Identity.Consumers;

public sealed class CreateEmailVerificationCodeConsumer : IConsumer<CreateEmailVerificationCodeCommand>
{
    readonly IMediator _mediator;

    public CreateEmailVerificationCodeConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateEmailVerificationCodeCommand> context)
    {
        var response = await _mediator.Send(context.Message);
        await context.RespondAsync(response);
    }
}

public sealed class CreateEmailVerificationCodeRequestedConsumer : IConsumer<CreateEmailVerificationCodeRequestedEvent>
{
    readonly IMediator _mediator;

    public CreateEmailVerificationCodeRequestedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateEmailVerificationCodeRequestedEvent> context)
    {
        await _mediator.Publish(context.Message);
    }
}
