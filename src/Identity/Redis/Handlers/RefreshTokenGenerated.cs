using MediatR;

namespace Nova.Identity.Handlers;

sealed class RefreshTokenGenerated_Handler : INotificationHandler<RefreshTokenGenerated>
{
    readonly MultiplexerProvider _multiplexerProvider;

    public RefreshTokenGenerated_Handler(MultiplexerProvider multiplexerProvider)
    {
        _multiplexerProvider = multiplexerProvider;
    }

    public async Task Handle(RefreshTokenGenerated notification, CancellationToken cancellationToken)
    {
        var database = _multiplexerProvider.Instance().GetDatabase();
        await database.StringSetAsync($"RefreshToken/{notification.AccessTokenId}", notification.TokenString);
    }
}
