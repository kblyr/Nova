namespace Nova.Identity.Handlers;

sealed class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand>
{
    readonly MultiplexerProvider _multiplexerProvider;
    readonly EmailVerificationCodeKeyGenerator _keyGenerator;
    readonly IMediator _mediator;

    public VerifyEmailHandler(MultiplexerProvider multiplexerProvider, EmailVerificationCodeKeyGenerator keyGenerator, IMediator mediator)
    {
        _multiplexerProvider = multiplexerProvider;
        _keyGenerator = keyGenerator;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var database = await _multiplexerProvider.GetDatabase();
        var key = _keyGenerator.Generate(request.Adapt<VerifyEmailCommand, EmailVerificationCodeKeyGenerator.Payload>());
        var modelString = await database.StringGetAsync(key);

        if (modelString.IsNullOrEmpty)
        {
            return request.Adapt<VerifyEmailCommand, EmailAddressNotFoundResponse>();
        }

        var model = JsonSerializer.Deserialize<EmailVerificationCodeModel>(modelString);
        if (model is null || model.VerificationCode != request.VerificationCode)
        {
            return request.Adapt<VerifyEmailCommand, IncorrectEmailVerificationCodeResponse>();
        }

        await database.KeyDeleteAsync(key);
        await _mediator.Publish(request.Adapt<VerifyEmailCommand, EmailVerifiedEvent>(), cancellationToken);
        return VerifyEmailCommand.Response.Instance;
    }
}
