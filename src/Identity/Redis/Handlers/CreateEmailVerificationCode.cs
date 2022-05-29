using Nova.Utilities;

namespace Nova.Identity.Handlers;

sealed class CreateEmailVerificationCodeHandler : IRequestHandler<CreateEmailVerificationCodeCommand>
{
    const int VerificationCodeLength = 7;

    readonly MultiplexerProvider _multiplexerProvider;
    readonly EmailVerificationCodeKeyGenerator _keyGenerator;
    readonly IRandomStringGenerator _randomStringGenerator;
    readonly IMediator _mediator;

    public CreateEmailVerificationCodeHandler(MultiplexerProvider multiplexerProvider, EmailVerificationCodeKeyGenerator keyGenerator, IRandomStringGenerator randomStringGenerator, IMediator mediator)
    {
        _multiplexerProvider = multiplexerProvider;
        _keyGenerator = keyGenerator;
        _randomStringGenerator = randomStringGenerator;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(CreateEmailVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var database = await _multiplexerProvider.GetDatabase();
        var key = _keyGenerator.Generate(request.Adapt<CreateEmailVerificationCodeCommand, EmailVerificationCodeKeyGenerator.Payload>());

        if (await database.KeyExistsAsync(key))
        {
            return request.Adapt<CreateEmailVerificationCodeCommand, EmailVerificationCodeAlreadyCreatedResponse>();
        }

        var model = request.Adapt<CreateEmailVerificationCodeCommand, EmailVerificationCodeModel>() with 
        {
            VerificationCode = _randomStringGenerator.Generate(VerificationCodeLength)
        };

        await database.StringSetAsync(key, JsonSerializer.Serialize(model));
        await _mediator.Publish(model.Adapt<EmailVerificationCodeModel, EmailVerificationCodeCreatedEvent>(), cancellationToken);
        return model.Adapt<EmailVerificationCodeModel, CreateEmailVerificationCodeCommand.Response>();
    }
}