using System.Text;
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
        var key = new StringBuilder("AccessToken")
            .Append($"/{notification.UserId}")
            .Append($"/{notification.ApplicationId}")
            .Append($"/{notification.AccessToken.Id:N}")
            .ToString();
        await database.StringSetAsync(key, notification.AccessToken.TokenString);
    }
}