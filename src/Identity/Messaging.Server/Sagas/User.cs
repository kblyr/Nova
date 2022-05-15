#nullable disable

using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Sagas;

public sealed class UserSaga : MassTransitStateMachine<UserSaga.Instance>
{
    readonly UserStatusesOptions _userStatuses;

    public UserSaga(IOptions<UserStatusesOptions> userStatuses)
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
            corr => corr
                .CorrelateBy((instance, context) => instance.CurrentData.Id == context.Message.Id)
                .SelectId(context => NewId.NextGuid())
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

        DuringAny(Ignore(UserAdded));

        During(Pending
        );

        During(Active
        );

        During(Deactivated
        );

        During(Locked
        );
    }

    void OnUserAdded(BehaviorContext<Instance, UserAddedEvent> context)
    {
        context.Saga.CurrentData.Id = context.Message.Id;
        context.Saga.CurrentData.StatusId = context.Message.StatusId;
    }

    public record Instance : SagaStateMachineInstance, ISagaVersion
    {
        Guid ISaga.CorrelationId { get; set; }
        int ISagaVersion.Version { get; set; }
        public string CurrentState { get; set; }
        public Data CurrentData { get; set; } = new();

        public record Data
        {
            public int Id { get; set; }
            public short StatusId { get; set; }
        }
    }
}