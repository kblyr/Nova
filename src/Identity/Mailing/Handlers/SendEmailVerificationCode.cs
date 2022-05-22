namespace Nova.Identity.Handlers;

sealed class SendEmailVerificationCodeHandler : IRequestHandler<SendEmailVerificationCodeCommand>
{
    readonly EmailVerificationCodeTemplateLoader _templateLoader;
    readonly EmailVerificationCodeSender _sender;
    readonly IMediator _mediator;

    public SendEmailVerificationCodeHandler(EmailVerificationCodeTemplateLoader templateLoader, EmailVerificationCodeSender sender, IMediator mediator)
    {
        _templateLoader = templateLoader;
        _sender = sender;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(SendEmailVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var model = request.Adapt<SendEmailVerificationCodeCommand, EmailVerificationCodeModel>();
        var template = await _templateLoader.Load(cancellationToken);
        var content = template.Render(model);
        await _sender.Send(request.EmailAddress, content, cancellationToken);
        await _mediator.Publish(request.Adapt<SendEmailVerificationCodeCommand, EmailVerificationCodeSentEvent>(), cancellationToken);
        return SendEmailVerificationCodeCommand.Response.Instance;
    }
}