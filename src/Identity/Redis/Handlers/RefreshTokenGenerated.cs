using System.Text;
using MediatR;

namespace Nova.Identity.Handlers;

sealed class RefreshTokenGenerated_Handler : INotificationHandler<RefreshTokenGenerated>
{
    readonly ConnectionMultiplexerFactory _multiplexerFactory;

    public RefreshTokenGenerated_Handler(ConnectionMultiplexerFactory multiplexerFactory)
    {
        _multiplexerFactory = multiplexerFactory;
    }

    public async Task Handle(RefreshTokenGenerated notification, CancellationToken cancellationToken)
    {
        using var multiplexer = await _multiplexerFactory.Connect();
        var database = multiplexer.GetDatabase();
        var key = new StringBuilder("RefreshToken")
            .Append($"/{notification.AccessTokenId:N}")
            .ToString();
        await database.StringSetAsync(key, notification.TokenString);
    }
}
