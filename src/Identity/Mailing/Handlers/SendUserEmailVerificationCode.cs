namespace Nova.Identity.Handlers;

sealed class SendUserEmailVerificationCodeHandler : IRequestHandler<SendUserEmailVerificationCodeCommand>
{
    readonly UserEmailVerificationCodeTemplateLoader _templateLoader;
    readonly UserEmailVerificationCodeSender _sender;
    readonly IMediator _mediator;

    public SendUserEmailVerificationCodeHandler(UserEmailVerificationCodeTemplateLoader templateLoader, UserEmailVerificationCodeSender sender, IMediator mediator)
    {
        _templateLoader = templateLoader;
        _sender = sender;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(SendUserEmailVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var model = request.Adapt<SendUserEmailVerificationCodeCommand, UserEmailVerificationCodeModel>();
        var template = await _templateLoader.Load(cancellationToken);
        var content = template.Render(model);
        await _sender.Send(request.EmailAddress, content, cancellationToken);
        await _mediator.Publish(request.Adapt<SendUserEmailVerificationCodeCommand, UserEmailVerificationCodeSentEvent>(), cancellationToken);
        return SendUserEmailVerificationCodeCommand.Response.Instance;
    }
}
