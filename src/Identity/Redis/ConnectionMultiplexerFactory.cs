using StackExchange.Redis;

namespace Nova.Identity;

sealed class ConnectionMultiplexerFactory
{
    readonly string _configuration;

    public ConnectionMultiplexerFactory(string configuration)
    {
        _configuration = configuration;
    }

    public async Task<IConnectionMultiplexer> Connect()
    {
        return await ConnectionMultiplexer.ConnectAsync(_configuration);
    }
}