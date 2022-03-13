using System.Text;
using MediatR;

namespace Nova.Identity.Handlers;

sealed class AccessTokenGenerated_Handler : INotificationHandler<AccessTokenGenerated>
{
    readonly ConnectionMultiplexerFactory _multiplexerFactory;

    public AccessTokenGenerated_Handler(ConnectionMultiplexerFactory multiplexerFactory)
    {
        _multiplexerFactory = multiplexerFactory;
    }

    public async Task Handle(AccessTokenGenerated notification, CancellationToken cancellationToken)
    {
        using var multiplexer = await _multiplexerFactory.Connect();
        var database = multiplexer.GetDatabase();
        var key = new StringBuilder("AccessToken")
            .Append($"/{notification.UserId}")
            .Append($"/{notification.ApplicationId}")
            .Append($"/{notification.AccessToken.Id:N}")
            .ToString();
        await database.StringSetAsync(key, notification.AccessToken.TokenString);
    }
}