using System.Text.Json;
using Nova.Identity.Utilities;

namespace Nova.Identity.Handlers;

sealed class CreateUserEmailAddressVerificationTokenRequestedHandler : INotificationHandler<CreateUserEmailAddressVerificationTokenRequestedEvent>
{
    readonly MultiplexerProvider _multiplexerProvider;
    readonly IKeyGenerator<UserEmailAddressVerificationTokenModel> _keyGenerator;
    readonly IUserEmailAddressVerificationTokenGenerator _tokenGenerator;
    readonly IMediator _mediator;

    public CreateUserEmailAddressVerificationTokenRequestedHandler(MultiplexerProvider multiplexerProvider, IKeyGenerator<UserEmailAddressVerificationTokenModel> keyGenerator, IUserEmailAddressVerificationTokenGenerator tokenGenerator, IMediator mediator)
    {
        _multiplexerProvider = multiplexerProvider;
        _keyGenerator = keyGenerator;
        _tokenGenerator = tokenGenerator;
        _mediator = mediator;
    }

    public async Task Handle(CreateUserEmailAddressVerificationTokenRequestedEvent notification, CancellationToken cancellationToken)
    {
        var database = await _multiplexerProvider.GetDatabase();
        var model = notification.Adapt<CreateUserEmailAddressVerificationTokenRequestedEvent, UserEmailAddressVerificationTokenModel>();
        var key = _keyGenerator.Generate(model);

        if (await database.KeyExistsAsync(key))
            return;

        model = model with { TokenString = _tokenGenerator.Generate() };
        await database.StringSetAsync(key, JsonSerializer.Serialize(model));
        await _mediator.Publish(model.Adapt<UserEmailAddressVerificationTokenModel, UserEmailAddressVerificationTokenCreatedEvent>(), cancellationToken);
    }
}
