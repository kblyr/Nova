using Nova.Utilities;

namespace Nova.Identity.Handlers;

sealed class CreateUserEmailVerificationCodeHandler : IRequestHandler<CreateUserEmailVerificationCodeCommand>
{
    const int VerificationCodeLength = 7;

    readonly MultiplexerProvider _multiplexerProvider;
    readonly UserEmailVerificationCodeKeyGenerator _keyGenerator;
    readonly IRandomStringGenerator _randomStringGenerator;
    readonly IMediator _mediator;

    public CreateUserEmailVerificationCodeHandler(MultiplexerProvider multiplexerProvider, UserEmailVerificationCodeKeyGenerator keyGenerator, IRandomStringGenerator randomStringGenerator, IMediator mediator)
    {
        _multiplexerProvider = multiplexerProvider;
        _keyGenerator = keyGenerator;
        _randomStringGenerator = randomStringGenerator;
        _mediator = mediator;
    }

    public async Task<IResponse> Handle(CreateUserEmailVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var database = await _multiplexerProvider.GetDatabase();
        var key = _keyGenerator.Generate(request.Adapt<CreateUserEmailVerificationCodeCommand, UserEmailVerificationCodeKeyGenerator.Payload>());

        if (await database.KeyExistsAsync(key))
        {
            return request.Adapt<CreateUserEmailVerificationCodeCommand, UserEmailVerificationCodeAlreadyCreatedResponse>();
        }

        var model = request.Adapt<CreateUserEmailVerificationCodeCommand, UserEmailVerificationCodeModel>() with
        {
            VerificationCode = _randomStringGenerator.Generate(VerificationCodeLength)
        };

        await database.StringSetAsync(key, JsonSerializer.Serialize(model));
        await _mediator.Publish(model.Adapt<UserEmailVerificationCodeModel, UserEmailVerificationCodeCreatedEvent>(), cancellationToken);
        return model.Adapt<UserEmailVerificationCodeModel, CreateUserEmailVerificationCodeCommand.Response>();
    }
}