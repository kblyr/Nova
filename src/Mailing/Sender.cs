using MimeKit;
using Nova.Configuration;

namespace Nova;

public abstract class MailSenderBase 
{
    readonly SmtpClient _smtp = new();
    readonly MailOptions _options;

    protected MailSenderBase(MailOptions options)
    {
        _options = options;
    }

    public async Task Connect(CancellationToken cancellationToken  = default)
    {
        if (_smtp.IsConnected)
        {
            return;
        }

        await _smtp.ConnectAsync(_options.Host, _options.Port, _options.SocketOptions, cancellationToken);
    }

    public async Task Disconnect(CancellationToken cancellationToken = default)
    {
        if (!_smtp.IsConnected)
        {
            return;
        }

        await _smtp.DisconnectAsync(true, cancellationToken);
    }

    public async Task Authenticate(CancellationToken cancellationToken = default)
    {
        if (_smtp.IsAuthenticated)
        {
            return;
        }

        await _smtp.AuthenticateAsync(_options.SenderAddress, _options.Password, cancellationToken);
    }

    public async Task Send(MailboxAddress recipientAddress, string content, CancellationToken cancellationToken = default)
    {
        await Connect(cancellationToken);
        await Authenticate(cancellationToken);

        if (!_smtp.IsConnected || !_smtp.IsAuthenticated)
        {
            return;
        }

        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_options.SenderAddress),
            Subject = _options.Subject
        };
        message.To.Add(recipientAddress);
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = content;
        message.Body = bodyBuilder.ToMessageBody();
        await _smtp.SendAsync(message, cancellationToken);
    }
}