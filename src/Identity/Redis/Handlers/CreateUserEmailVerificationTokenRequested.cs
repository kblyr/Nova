using System.Text.Json;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;
using Nova.KeyGenerators;

namespace Nova.Identity.Handlers;

sealed class CreateUserEmailVerificationTokenRequestedHandler : INotificationHandler<CreateUserEmailVerificationTokenRequestedEvent>
{
    readonly MultiplexerProvider _multiplexer;
    readonly UserEmailVerificationConfig _config;
    readonly IKeyGenerator<IUserEmailVerificationPayload> _keyGenerator;
    readonly IMapper _mapper;
    readonly IMediator _mediator;

    public CreateUserEmailVerificationTokenRequestedHandler(MultiplexerProvider multiplexer, IOptions<UserEmailVerificationConfig> config, IKeyGenerator<IUserEmailVerificationPayload> keyGenerator, IMapper mapper, IMediator mediator)
    {
        _multiplexer = multiplexer;
        _config = config.Value;
        _keyGenerator = keyGenerator;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Handle(CreateUserEmailVerificationTokenRequestedEvent notification, CancellationToken cancellationToken)
    {
        var database = await _multiplexer.GetDatabase();
        var key = _keyGenerator.Generate(notification);

        if (await database.KeyExistsAsync(key))
            return;

        var token = _mapper.Map<CreateUserEmailVerificationTokenRequestedEvent, UserEmailVerificationToken>(notification) with
        {
            TokenString = Guid.NewGuid().ToString(),
            ResendOn = DateTimeOffset.UtcNow.Add(_config.ResendInterval)
        };

        await database.StringSetAsync(key, JsonSerializer.Serialize(token), _config.Expiration);
        var createdEvent = _mapper.Map<UserEmailVerificationToken, UserEmailVerificationTokenCreatedEvent>(token);
        await _mediator.Publish(createdEvent);
    }
}
