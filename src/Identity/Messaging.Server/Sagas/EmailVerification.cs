#nullable disable
namespace Nova.Identity.Sagas;

public sealed class EmailVerificationSaga : MassTransitStateMachine<EmailVerificationSaga.Instance>
{
    readonly ILogger<EmailVerificationSaga> _logger;

    public EmailVerificationSaga(ILogger<EmailVerificationSaga> logger)
    {
        _logger = logger;
        InstanceState(instance => instance.CurrentState);
        ConfigureEvents();
        ConfigureEventActivities();
    }

    public State VerificationCodeCreated { get; private set; }
    public State VerificationCodeSent { get; private set; }
    public State Verified { get; private set; }

    public Event<EmailVerificationCodeCreatedEvent> EmailVerificationCodeCreated { get; private set; }
    public Event<EmailVerificationCodeSentEvent> EmailVerificationCodeSent { get; private set; }
    public Event<EmailVerifiedEvent> EmailVerified { get; private set; }

    void ConfigureEvents()
    {
        Event(() => EmailVerificationCodeCreated,
            corr => corr
                .CorrelateBy((instance, context) => instance.Data.EmailAddress == context.Message.EmailAddress)
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => EmailVerificationCodeSent, 
            corr => corr.CorrelateBy((instance, context) => instance.Data.EmailAddress == context.Message.EmailAddress)
        );

        Event(() => EmailVerified, 
            corr => corr.CorrelateBy((instance, context) => instance.Data.EmailAddress == context.Message.EmailAddress)
        );
    }

    void ConfigureEventActivities()
    {
        Initially(
            Ignore(EmailVerificationCodeSent),
            Ignore(EmailVerified),
            When(EmailVerificationCodeCreated)
                .Then(context => {_logger.LogInformation("Verification Code was created");
                    context.Saga.Data.EmailAddress = context.Message.EmailAddress;
                    context.Saga.Data.VerificationCode = context.Message.VerificationCode;
                })
                .Publish(context => context.Message.Adapt<EmailVerificationCodeCreatedEvent, SendEmailVerificationCodeRequestedEvent>())
                .TransitionTo(VerificationCodeCreated)
        );

        During(VerificationCodeCreated,
            Ignore(EmailVerificationCodeCreated),
            When(EmailVerificationCodeSent)
                .TransitionTo(VerificationCodeSent),
            When(EmailVerified)
                .TransitionTo(Verified)
        );

        During(VerificationCodeSent,
            Ignore(EmailVerificationCodeCreated),
            Ignore(EmailVerificationCodeSent),
            When(EmailVerified)
                .TransitionTo(Verified)
        );
    }

    public sealed class Instance : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }    
        public int Version { get; set; }
        public string CurrentState { get; set; }
        public InstanceData Data { get; set; } = new();
    }

    public sealed class InstanceData
    {
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
    }
}