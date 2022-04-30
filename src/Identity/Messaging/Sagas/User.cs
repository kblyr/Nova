#nullable disable

namespace Nova.Identity.Sagas;

public sealed class UserSaga : MassTransitStateMachine<UserSaga.Instance>
{
    readonly UserStatusesConfig _userStatuses;

    public UserSaga(IOptions<UserStatusesConfig> userStatuses)
    {
        _userStatuses = userStatuses.Value;
        InstanceState(instance => instance.CurrentState);
        ConfigureEvents();
        ConfigureEventActivities();
    }

    public State Pending { get; private set; }
    public State Active { get; private set; }
    public State Deactivated { get; private set; }
    public State Locked { get; private set; }

    public Event<UserAddedEvent> UserAdded { get; private set; }

    void ConfigureEvents()
    {
        Event(() => UserAdded,
            corr => corr.CorrelateBy((instance, context) => instance.UserId == context.Message.Id).SelectId(context => NewId.NextGuid())
        );
    }

    void ConfigureEventActivities()
    {
        Initially(
            When(UserAdded, context => context.Message.StatusId == _userStatuses.Pending)
                .Then(OnUserAdded)
                .TransitionTo(Pending),
            When(UserAdded, context => context.Message.StatusId == _userStatuses.Active)
                .Then(OnUserAdded)
                .TransitionTo(Active),
            When(UserAdded, context => context.Message.StatusId == _userStatuses.Deactivated)
                .Then(OnUserAdded)
                .TransitionTo(Deactivated),
            When(UserAdded, context => context.Message.StatusId == _userStatuses.Locked)
                .Then(OnUserAdded)
                .TransitionTo(Locked)
        );

        During(Pending,
            Ignore(UserAdded)
        );

        During(Active,
            Ignore(UserAdded)
        );

        During(Deactivated,
            Ignore(UserAdded)
        );

        During(Locked,
            Ignore(UserAdded)
        );
    }

    void OnUserAdded(BehaviorContext<Instance, UserAddedEvent> context)
    {
        context.Saga.UserId = context.Message.Id;
        context.Saga.StatusId = context.Message.StatusId;
    }

    public sealed class Instance : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public int Version { get; set; }
        public string CurrentState { get; set; }

        public int UserId { get; set; }
        public short StatusId { get; set; }
    }
}