namespace Nova;

public abstract class MultiplexerProviderBase 
{
    readonly string _configuration;

    protected MultiplexerProviderBase(string configuration)
    {
        _configuration = configuration;
    }

    IConnectionMultiplexer? _instance;
    public async Task<IConnectionMultiplexer> GetInstance()
    {
        if (_instance is not null)
            return _instance;

        _instance = await ConnectionMultiplexer.ConnectAsync(_configuration);
        return _instance;
    }

    public async Task<IDatabase> GetDatabase()
    {
        var instance = await GetInstance();
        return instance.GetDatabase();
    }
}