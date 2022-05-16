namespace Nova.Identity.Handlers;

sealed class SendUserEmailAddressVerificationRequestedHandler : INotificationHandler<SendUserEmailAddressVerificationRequestedEvent>
{
    readonly UserEmailAddressVerificationTemplateLoader _templateLoader;
    readonly IMediator _mediator;
    readonly UserEmailAddressVerificationSender _sender;

    public SendUserEmailAddressVerificationRequestedHandler(UserEmailAddressVerificationTemplateLoader templateLoader, IMediator mediator, UserEmailAddressVerificationSender sender)
    {
        _templateLoader = templateLoader;
        _mediator = mediator;
        _sender = sender;
    }

    public async Task Handle(SendUserEmailAddressVerificationRequestedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var template = await _templateLoader.Load(cancellationToken);
            var body = await template.RenderAsync(new { Link = notification.Link });
            await _sender.Send(MailboxAddress.Parse(notification.EmailAddress), body, cancellationToken);
            await _mediator.Publish(notification.Adapt<SendUserEmailAddressVerificationRequestedEvent, UserEmailAddressVerificationSentEvent>(), cancellationToken);
        }
        catch (Exception)
        {
            await _mediator.Publish(notification.Adapt<SendUserEmailAddressVerificationRequestedEvent, SendUserEmailAddressVerificationFailedEvent>(), cancellationToken);
        }
    }
}
