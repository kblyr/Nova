namespace Nova;

public interface ISmtpClientAdapter<TPayload> : IDisposable
{
    Task<IResult> Connect();
    Task<IResult> Send(MimeMessage message);
}

sealed class SmtpClientAdapter<TPayload> : ISmtpClientAdapter<TPayload>
{
    readonly SmtpClient _client = new();
    readonly MailConnectionConfig<TPayload> _connectionConfig;

    public SmtpClientAdapter(MailConnectionConfig<TPayload> connectionConfig)
    {
        _connectionConfig = connectionConfig;
    }

    public async Task<IResult> Connect()
    {
        if (_client.IsConnected)
            return SmtpClientConnectedResult.Instance;
        
        try 
        {
            await _client.ConnectAsync(_connectionConfig.Host, _connectionConfig.Port);
            return SmtpClientConnectedResult.Instance;
        }
        catch (Exception ex)
        {
            return new ExceptionThrownResult(ex);
        }
    }

    public async Task<IResult> Send(MimeMessage message)
    {
        var connectResult = await Connect();

        if (connectResult is not SmtpClientConnectedResult)
            return connectResult;

        try
        {
            await _client.SendAsync(message);
            return new MimeMessageSentResult(message);
        }
        catch (Exception ex)
        {
            return new ExceptionThrownResult(ex);
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
