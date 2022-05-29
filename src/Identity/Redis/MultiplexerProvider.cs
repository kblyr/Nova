namespace Nova.Identity;

public sealed class MultiplexerProvider : MultiplexerProviderBase, IMultiplexerProvider
{
    public MultiplexerProvider(string configuration) : base(configuration)
    {
    }
}