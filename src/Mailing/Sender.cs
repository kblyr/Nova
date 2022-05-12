namespace Nova;

public interface IMailSender<TPayload>
{
    Task<IResult> Send(TPayload payload);
}

sealed class MailSender<TPayload> : IMailSender<TPayload>
{
    readonly IMailComposer<TPayload> _composer;
    readonly ISmtpClientAdapter<TPayload> _smtpClient;

    public MailSender(IMailComposer<TPayload> composer, ISmtpClientAdapter<TPayload> smtpClient)
    {
        _composer = composer;
        _smtpClient = smtpClient;
    }

    public async Task<IResult> Send(TPayload payload)
    {
        var composerResult = await _composer.Compose(payload);
        if (composerResult is not MimeMessageComposedResult messageComposedResult)
            return composerResult;

        return await _smtpClient.Send(messageComposedResult.Message);
    }
}
