using StackExchange.Redis;

namespace Nova;

public abstract class MultiplexerProviderBase
{
    readonly string _configuration;

    public MultiplexerProviderBase(string configuration)
    {
        _configuration = configuration;
    }

    private IConnectionMultiplexer? _instance;

    public IConnectionMultiplexer Instance()
    {
        if (_instance is not null)
            return _instance;

        _instance = ConnectionMultiplexer.Connect(_configuration);
        return _instance;
    }
}