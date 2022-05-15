using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Handlers;

sealed class SendUserEmailAddressVerificationRequestedHandler : INotificationHandler<SendUserEmailAddressVerificationRequestedEvent>
{
    readonly UserEmailAddressVerificationTemplateLoader _templateLoader;
    readonly IMediator _mediator;
    readonly UserEmailAddressVerificationMailOptions _options;

    public SendUserEmailAddressVerificationRequestedHandler(UserEmailAddressVerificationTemplateLoader templateLoader, IMediator mediator, IOptions<UserEmailAddressVerificationMailOptions> options)
    {
        _templateLoader = templateLoader;
        _mediator = mediator;
        _options = options.Value;
    }

    public async Task Handle(SendUserEmailAddressVerificationRequestedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var template = await _templateLoader.Load(cancellationToken);
            var message = new MimeMessage();
            message.Sender = MailboxAddress.Parse(_options.SenderAddress);
            var body = await template.RenderAsync(new { Link = notification.Link });
            await _mediator.Publish(notification.Adapt<SendUserEmailAddressVerificationRequestedEvent, UserEmailAddressVerificationSentEvent>(), cancellationToken);
        }
        catch (Exception)
        {
            await _mediator.Publish(notification.Adapt<SendUserEmailAddressVerificationRequestedEvent, SendUserEmailAddressVerificationFailedEvent>(), cancellationToken);
        }
    }
}
