namespace Nova.Identity.Handlers;

sealed class VerifyUserEmailHandler : IRequestHandler<VerifyUserEmailCommand>
{
    readonly MultiplexerProvider _multiplexerProvider;
    readonly UserEmailVerificationCodeKeyGenerator _keyGenerator;
    readonly IMediator _mediator;

    public VerifyUserEmailHandler(MultiplexerProvider multiplexerProvider, UserEmailVerificationCodeKeyGenerator keyGenerator, IMediator mediator)
    {
        _multiplexerProvider = multiplexerProvider;
        _keyGenerator = keyGenerator;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(VerifyUserEmailCommand request, CancellationToken cancellationToken)
    {
        var database = await _multiplexerProvider.GetDatabase();
        var key = _keyGenerator.Generate(request.Adapt<VerifyUserEmailCommand, UserEmailVerificationCodeKeyGenerator.Payload>());
        var modelString = await database.StringGetAsync(key);
        if (modelString.IsNullOrEmpty)
        {
            return request.Adapt<VerifyUserEmailCommand, UserEmailAddressNotFoundResponse>();
        }

        var model = JsonSerializer.Deserialize<UserEmailVerificationCodeModel>(modelString);
        if (model is null || model.VerificationCode != request.VerificationCode)
        {
            return request.Adapt<VerifyUserEmailCommand, IncorrectUserEmailVerificationCodeResponse>();
        }

        await database.KeyDeleteAsync(key);
        await _mediator.Publish(request.Adapt<VerifyUserEmailCommand, UserEmailVerifiedEvent>(), cancellationToken);
        return request.Adapt<VerifyUserEmailCommand, VerifyUserEmailCommand.Response>();
    }
}
