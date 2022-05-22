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

    void ConfigureEvents()
    {
        Event(() => UserSignedUp,
            corr => corr
                .CorrelateBy((instance, context) => instance.Data.Id == context.Message.Id)
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailVerificationCodeCreated,
            corr => corr
                .CorrelateBy((instance, context) => instance.Data.Id == context.Message.Id)
                .SelectId(context => NewId.NextGuid())
        );
    }

    void ConfigureEventActivities()
    {
        Initially(
            Ignore(UserSignedUp, context => context.Message.StatusId == _userStatuses.Active),
            When(UserSignedUp, context => context.Message.StatusId != _userStatuses.Pending)
                .Then(context => {
                    context.Saga.Data.Id = context.Message.Id;
                    context.Saga.Data.EmailAddress = context.Message.EmailAddress;
                })
                .Publish(context => context.Message.Adapt<UserSignedUpEvent, CreateUserEmailVerificationCodeRequestedEvent>())
                .TransitionTo(Pending),
            When(UserEmailVerificationCodeCreated)
                .Then(context => {
                    context.Saga.Data.Id = context.Message.Id;
                    context.Saga.Data.EmailAddress = context.Message.EmailAddress;
                })
                .TransitionTo(VerificationCodeCreated)
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
        public int Id { get; set; }
        public string EmailAddress { get; set; }
    }
}