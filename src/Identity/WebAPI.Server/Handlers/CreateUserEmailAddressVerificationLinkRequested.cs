using Nova.Identity.Utilities;

namespace Nova.Identity.Handlers;

sealed class CreateUserEmailAddressVerificationLinkRequestedHandler : INotificationHandler<CreateUserEmailAddressVerificationLinkRequestedEvent>
{
    readonly IMediator _mediator;
    readonly IUserEmailAddressVerificationLinkCreator _linkCreator;

    public CreateUserEmailAddressVerificationLinkRequestedHandler(IMediator mediator, IUserEmailAddressVerificationLinkCreator linkCreator)
    {
        _mediator = mediator;
        _linkCreator = linkCreator;
    }

    public async Task Handle(CreateUserEmailAddressVerificationLinkRequestedEvent notification, CancellationToken cancellationToken)
    {
        var link = _linkCreator.Create(notification.UserId, notification.EmailAddress, notification.TokenString);
        await _mediator.Publish(notification.Adapt<CreateUserEmailAddressVerificationLinkRequestedEvent, UserEmailAddressVerificationLinkCreatedEvent>() with 
        {
            Link = link
        }, cancellationToken);
    }
}
