using MediatR;

namespace Nova.Identity.Handlers;

sealed class AccessTokenGenerated_Handler : INotificationHandler<AccessTokenGenerated>
{
    readonly MultiplexerProvider _multiplexerProvider;

    public AccessTokenGenerated_Handler(MultiplexerProvider multiplexerProvider)
    {
        _multiplexerProvider = multiplexerProvider;
    }

    public async Task Handle(AccessTokenGenerated notification, CancellationToken cancellationToken)
    {
        var database = _multiplexerProvider.Instance().GetDatabase();
        await database.StringSetAsync($"AccessToken/{notification.AccessToken.Id}", notification.AccessToken.TokenString);
    }
}