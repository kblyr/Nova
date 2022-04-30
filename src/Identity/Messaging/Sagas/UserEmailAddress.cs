#nullable disable

using AutoMapper;

namespace Nova.Identity.Sagas;

public sealed class UserEmailAddressSaga : MassTransitStateMachine<UserEmailAddressSaga.Instance>
{
    readonly IMapper _mapper;

    public UserEmailAddressSaga(IMapper mapper)
    {
        _mapper = mapper;
        InstanceState(instance => instance.CurrentState);
        ConfigureEvents();
        ConfigureEventActivities();
    }

    public State Pending { get; private set; }
    public State VerificationSent { get; private set; }
    public State Verified { get; private set; }

    public Event<UserEmailAddressAddedEvent> UserEmailAddressAdded { get; private set; }
    public Event<EmailVerificationSentToUserEvent> EmailVerificationSentToUser { get; private set; }
    public Event<UserEmailAddressVerifiedEvent> UserEmailAddressVerified { get; private set; }

    void ConfigureEvents()
    {
        Event(() => UserEmailAddressAdded,
            corr => corr.CorrelateBy((instance, context) =>
                instance.UserId == context.Message.UserId
                && instance.EmailAddress == context.Message.EmailAddress
            ).SelectId(context => NewId.NextGuid())
        );

        Event(() => EmailVerificationSentToUser,
            corr => corr.CorrelateBy((instance, context) =>
                instance.UserId == context.Message.UserId
                && instance.EmailAddress == context.Message.EmailAddress
            ).SelectId(context => NewId.NextGuid())
        );

        Event(() => UserEmailAddressVerified,
            corr => corr.CorrelateBy((instance, context) =>
                instance.UserId == context.Message.UserId
                && instance.EmailAddress == context.Message.EmailAddress
            ).SelectId(context => NewId.NextGuid())
        );
    }

    void ConfigureEventActivities()
    {
        Initially(
            When(UserEmailAddressAdded, context => context.Message.IsVerified)
                .Then(OnUserEmailAddressAdded)
                .TransitionTo(Verified),
            When(UserEmailAddressAdded, context => !context.Message.IsVerified)
                .Then(OnUserEmailAddressAdded)
                .Publish(context => _mapper.Map<UserEmailAddressAddedEvent, UserEmailVerificationSendRequestedEvent>(context.Message))
                .TransitionTo(Pending),
            When(EmailVerificationSentToUser)
                .Then(OnEmailVerificationSentToUser)
                .TransitionTo(VerificationSent),
            When(UserEmailAddressVerified)
                .Then(OnUserEmailAddressVerified)
                .TransitionTo(Verified)
        );

        During(Pending,
            Ignore(UserEmailAddressAdded),
            When(EmailVerificationSentToUser)
                .Then(OnEmailVerificationSentToUser)
                .TransitionTo(VerificationSent),
            When(UserEmailAddressVerified)
                .Then(OnUserEmailAddressVerified)
                .TransitionTo(Verified)
        );

        During(VerificationSent,
            Ignore(UserEmailAddressAdded),
            Ignore(EmailVerificationSentToUser),
            When(UserEmailAddressVerified)
                .Then(OnUserEmailAddressVerified)
                .TransitionTo(Verified)
        );

        During(Verified,
            Ignore(UserEmailAddressAdded),
            Ignore(EmailVerificationSentToUser),
            Ignore(UserEmailAddressVerified)
        );
    }

    void OnUserEmailAddressAdded(BehaviorContext<Instance, UserEmailAddressAddedEvent> context)
    {
        context.Saga.UserId = context.Message.UserId;
        context.Saga.EmailAddress = context.Message.EmailAddress;
        context.Saga.IsVerified = context.Message.IsVerified;
    }

    void OnEmailVerificationSentToUser(BehaviorContext<Instance, EmailVerificationSentToUserEvent> context)
    {
        context.Saga.UserId = context.Message.UserId;
        context.Saga.EmailAddress = context.Message.EmailAddress;
    }

    void OnUserEmailAddressVerified(BehaviorContext<Instance, UserEmailAddressVerifiedEvent> context)
    {
        context.Saga.UserId = context.Message.UserId;
        context.Saga.EmailAddress = context.Message.EmailAddress;
        context.Saga.IsVerified = true;
    }
    
    public sealed class Instance : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public int Version { get; set; }
        public string CurrentState { get; set; }

        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsVerified { get; set; }
    }
}