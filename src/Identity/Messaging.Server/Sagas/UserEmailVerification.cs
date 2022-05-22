#nullable disable
namespace Nova.Identity.Sagas;

public sealed class UserEmailVerificationSaga : MassTransitStateMachine<UserEmailVerificationSaga.Instance>
{
    readonly UserStatusesLookup _userStatuses;

    public UserEmailVerificationSaga(IOptions<UserStatusesLookup> userStatuses)
    {
        _userStatuses = userStatuses.Value;
        InstanceState(instance => instance.CurrentState);
        ConfigureEvents();
        ConfigureEventActivities();
    }

    public State Pending { get; private set; }
    public State VerificationCodeCreated { get; private set; }
    public State VerificationCodeSent { get; private set; }
    public State Verified { get; private set; }

    public Event<UserSignedUpEvent> UserSignedUp { get; private set; }
    public Event<UserEmailVerificationCodeCreatedEvent> UserEmailVerificationCodeCreated { get; private set; }
    public Event<UserEmailVerificationCodeSentEvent> UserEmailVerificationCodeSent { get; private set; }
    public Event<UserEmailVerifiedEvent> UserEmailVerified { get; private set; }

    void ConfigureEvents()
    {
        Event(() => UserSignedUp,
            corr => corr
                .CorrelateBy((instance, context) => 
                    instance.Data.UserId == context.Message.Id
                    && instance.Data.EmailAddress == context.Message.EmailAddress
                )
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailVerificationCodeCreated,
            corr => corr
                .CorrelateBy((instance, context) => 
                    instance.Data.UserId == context.Message.UserId
                    && instance.Data.EmailAddress == context.Message.EmailAddress
                )
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailVerificationCodeSent,
            corr => corr
                .CorrelateBy((instance, context) => 
                    instance.Data.UserId == context.Message.UserId
                    && instance.Data.EmailAddress == context.Message.EmailAddress
                )
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailVerified,
            corr => corr
                .CorrelateBy((instance, context) => 
                    instance.Data.UserId == context.Message.UserId
                    && instance.Data.EmailAddress == context.Message.EmailAddress
                )
                .OnMissingInstance(context => context.Discard())
        );
    }

    void ConfigureEventActivities()
    {
        Initially(
            Ignore(UserSignedUp, context => context.Message.StatusId != _userStatuses.Pending),
            Ignore(UserEmailVerified),
            When(UserSignedUp, context => context.Message.StatusId == _userStatuses.Pending)
                .Then(context => {
                    context.Saga.Data.UserId = context.Message.Id;
                    context.Saga.Data.EmailAddress = context.Message.EmailAddress;
                })
                .Publish(context => context.Message.Adapt<UserSignedUpEvent, CreateUserEmailVerificationCodeRequestedEvent>())
                .TransitionTo(Pending),
            When(UserEmailVerificationCodeCreated)
                .Then(context => {
                    context.Saga.Data.UserId = context.Message.UserId;
                    context.Saga.Data.EmailAddress = context.Message.EmailAddress;
                })
                .Publish(context => context.Message.Adapt<UserEmailVerificationCodeCreatedEvent, SendUserEmailVerificationCodeRequestedEvent>())
                .TransitionTo(VerificationCodeCreated),
            When(UserEmailVerificationCodeSent)
                .Then(context => {
                    context.Saga.Data.UserId = context.Message.UserId;
                    context.Saga.Data.EmailAddress = context.Message.EmailAddress;
                })
                .TransitionTo(VerificationCodeSent)
        );

        DuringAny(
            When(UserEmailVerified)
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
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
    }
}