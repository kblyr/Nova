using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Handlers;

sealed class AccessTokenGenerated_Handler : INotificationHandler<AccessTokenGenerated>
{
    readonly MultiplexerProvider _multiplexerProvider;
    readonly AccessTokenConfig _config;

    public AccessTokenGenerated_Handler(MultiplexerProvider multiplexerProvider, IOptions<AccessTokenConfig> config)
    {
        _multiplexerProvider = multiplexerProvider;
        _config = config.Value;
    }

    public async Task Handle(AccessTokenGenerated notification, CancellationToken cancellationToken)
    {
        var database = _multiplexerProvider.Instance().GetDatabase();
        var key = new StringBuilder("AccessToken")
            .Append($"/{notification.AccessToken.Id:N}")
            .ToString();
        await database.StringSetAsync(key, notification.AccessToken.TokenString, _config.Expiration);
    }
}