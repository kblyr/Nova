namespace Nova.Identity.Consumers;

public sealed class SendEmailVerificationCodeRequestedConsumer : IConsumer<SendEmailVerificationCodeRequestedEvent>
{
    readonly IMediator _mediator;
    readonly ILogger<SendEmailVerificationCodeRequestedConsumer> _logger;

    public SendEmailVerificationCodeRequestedConsumer(IMediator mediator, ILogger<SendEmailVerificationCodeRequestedConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SendEmailVerificationCodeRequestedEvent> context)
    {
        _logger.LogInformation("Consuming Event: {event}", nameof(SendEmailVerificationCodeRequestedEvent));
        _logger.LogInformation("Event Data: {data}", context.Message);
        await _mediator.Publish(context.Message);
    }
}