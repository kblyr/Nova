namespace Nova.Identity.Handlers;

sealed class UserEmailAddressVerifiedHandler : INotificationHandler<UserEmailAddressVerifiedEvent>
{
    readonly MultiplexerProvider _multiplexerProvider;
    readonly IKeyGenerator<UserEmailAddressVerificationTokenModel> _keyGenerator;

    public UserEmailAddressVerifiedHandler(MultiplexerProvider multiplexerProvider, IKeyGenerator<UserEmailAddressVerificationTokenModel> keyGenerator)
    {
        _multiplexerProvider = multiplexerProvider;
        _keyGenerator = keyGenerator;
    }

    public async Task Handle(UserEmailAddressVerifiedEvent notification, CancellationToken cancellationToken)
    {
        var database = await _multiplexerProvider.GetDatabase();
        var model = notification.Adapt<UserEmailAddressVerifiedEvent, UserEmailAddressVerificationTokenModel>();
        var key = _keyGenerator.Generate(model);
        await database.KeyDeleteAsync(key);
    }
}
