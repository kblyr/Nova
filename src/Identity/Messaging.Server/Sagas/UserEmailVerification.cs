#nullable disable

using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Sagas;

public sealed class UserEmailVerificationSaga : MassTransitStateMachine<UserEmailVerificationSaga.Instance>
{
    readonly UserEmailAddressStatusesOptions _statuses;

    public UserEmailVerificationSaga(IOptions<UserEmailAddressStatusesOptions> statuses)
    {
        _statuses = statuses.Value;

        InstanceState(instance => instance.CurrentState);
        ConfigureEvents();
    }

    public State Pending { get; private set; }
    public State VerificationTokenCreated { get; private set; }
    public State VerificationLinkCreated { get; private set; }
    public State VerificationSent { get; private set; }
    public State Verified { get; private set; }

    public Event<UserEmailAddressAddedEvent> UserEmailAddressAdded { get; private set; }
    public Event<UserEmailAddressVerificationTokenCreatedEvent> UserEmailAddressVerificationTokenCreated { get; private set; }
    public Event<UserEmailAddressVerificationLinkCreatedEvent> UserEmailAddressVerificationLinkCreated { get; private set; }
    public Event<UserEmailAddressVerificationSentEvent> UserEmailAddressVerificationSent { get; private set; }
    public Event<UserEmailAddressVerifiedEvent> UserEmailAddressVerified { get; private set; }

    void ConfigureEvents()
    {
        Event(() => UserEmailAddressAdded,
            corr => corr
                .CorrelateBy((instance, context) => 
                    instance.CurrentData.UserId == context.Message.UserId 
                    && instance.CurrentData.EmailAddress == context.Message.EmailAddress
                )
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailAddressVerificationTokenCreated,
            corr => corr
                .CorrelateBy((instance, context) =>
                    instance.CurrentData.UserId == context.Message.UserId
                    && instance.CurrentData.EmailAddress == context.Message.EmailAddress
                )
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailAddressVerificationLinkCreated,
            corr => corr
                .CorrelateBy((instance, context) =>
                    instance.CurrentData.UserId == context.Message.UserId
                    && instance.CurrentData.EmailAddress == context.Message.EmailAddress
                )
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailAddressVerificationSent,
            corr => corr
                .CorrelateBy((instance, context) =>
                    instance.CurrentData.UserId == context.Message.UserId
                    && instance.CurrentData.EmailAddress == context.Message.EmailAddress
                )
                .SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailAddressVerified,
            corr => corr
                .CorrelateBy((instance, context) =>
                    instance.CurrentData.UserId == context.Message.UserId
                    && instance.CurrentData.EmailAddress == context.Message.EmailAddress 
                )
                .OnMissingInstance(config => config.Discard())
        );
    }

    void ConfigureEventActivities()
    {
        Initially(
            Ignore(UserEmailAddressAdded, context => context.Message.StatusId == _statuses.Verified),
            When(UserEmailAddressAdded, context => context.Message.StatusId == _statuses.Pending)
                .Then(OnUserEmailAddressAdded)
                .Publish(context => context.Message.Adapt<UserEmailAddressAddedEvent, CreateUserEmailAddressVerificationTokenRequestedEvent>())
                .TransitionTo(Pending),
            When(UserEmailAddressVerificationTokenCreated)
                .Then(context => context.Saga.CurrentData.StatusId = _statuses.Pending)
                .Publish(context => context.Message.Adapt<UserEmailAddressVerificationTokenCreatedEvent, CreateUserEmailAddressVerificationLinkRequestedEvent>())
                .TransitionTo(VerificationTokenCreated),
            When(UserEmailAddressVerificationLinkCreated)
                .Then(context => context.Saga.CurrentData.StatusId = _statuses.Pending)
                .Publish(context => context.Message.Adapt<UserEmailAddressVerificationLinkCreatedEvent, SendUserEmailAddressVerificationRequestedEvent>())
                .TransitionTo(VerificationLinkCreated),
            When(UserEmailAddressVerificationSent)
                .Then(context => context.Saga.CurrentData.StatusId = _statuses.Pending)
                .TransitionTo(VerificationSent),
            When(UserEmailAddressVerified)
                .Then(context => context.Saga.CurrentData.StatusId = _statuses.Verified)
                .TransitionTo(Verified)
        );

        DuringAny(Ignore(UserEmailAddressAdded));

        During(VerificationTokenCreated,
            Ignore(UserEmailAddressVerificationTokenCreated)
        );

        During(VerificationLinkCreated,
            Ignore(UserEmailAddressVerificationTokenCreated),
            Ignore(UserEmailAddressVerificationLinkCreated)
        );

        During(VerificationSent,
            Ignore(UserEmailAddressVerificationTokenCreated),
            Ignore(UserEmailAddressVerificationLinkCreated),
            Ignore(UserEmailAddressVerificationSent)
        );

        During(Verified,
            Ignore(UserEmailAddressVerificationTokenCreated),
            Ignore(UserEmailAddressVerificationLinkCreated),
            Ignore(UserEmailAddressVerificationSent),
            Ignore(UserEmailAddressVerified)
        );
    }

    void OnUserEmailAddressAdded(BehaviorContext<Instance, UserEmailAddressAddedEvent> context)
    {
        context.Saga.CurrentData.UserId = context.Message.UserId;
        context.Saga.CurrentData.EmailAddress = context.Message.EmailAddress;
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
            public int UserId { get; set; }
            public string EmailAddress { get; set; }
            public short StatusId { get; set; }
        }
    }
}