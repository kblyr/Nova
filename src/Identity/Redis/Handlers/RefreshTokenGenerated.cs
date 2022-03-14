using System.Text;
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
        var key = new StringBuilder("RefreshToken")
            .Append($"/{notification.AccessTokenId:N}")
            .ToString();
        await database.StringSetAsync(key, notification.TokenString);
    }
}
