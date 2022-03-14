using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Nova.Identity.Configuration;

namespace Nova.Identity.Handlers;

sealed class RefreshTokenGenerated_Handler : INotificationHandler<RefreshTokenGenerated>
{
    readonly MultiplexerProvider _multiplexerProvider;
    readonly RefreshTokenConfig _config;

    public RefreshTokenGenerated_Handler(MultiplexerProvider multiplexerProvider, IOptions<RefreshTokenConfig> config)
    {
        _multiplexerProvider = multiplexerProvider;
        _config = config.Value;
    }

    public async Task Handle(RefreshTokenGenerated notification, CancellationToken cancellationToken)
    {
        var database = _multiplexerProvider.Instance().GetDatabase();
        var key = new StringBuilder("RefreshToken")
            .Append($"/{notification.AccessTokenId:N}")
            .ToString();
        await database.StringSetAsync(key, notification.TokenString, _config.Expiration);
    }
}
