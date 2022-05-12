namespace Nova.Identity.Handlers;

sealed class UserEmailVerificationTokenCreatedHandler : INotificationHandler<UserEmailVerificationTokenCreatedEvent>
{
    readonly IMailSender<IUserEmailVerificationPayload> _sender;
    readonly IMediator _mediator;

    public UserEmailVerificationTokenCreatedHandler(IMailSender<IUserEmailVerificationPayload> sender, IMediator mediator)
    {
        _sender = sender;
        _mediator = mediator;
    }

    public async Task Handle(UserEmailVerificationTokenCreatedEvent notification, CancellationToken cancellationToken)
    {
        var sendResult = await _sender.Send(notification);
        if (sendResult is not MimeMessageSentResult sentResult)
        {
            await 
            return;
        }
    }
}