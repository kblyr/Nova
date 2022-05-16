using MailKit.Security;

namespace Nova.Configuration;

public record MailOptions
{
    public string Host { get; init; } = "";
    public int Port { get; init; }
    public SecureSocketOptions SocketOptions { get; init; }
    public string SenderAddress { get; init; } = "";
    public string Password { get; init; } = "";

    public string TemplatePath { get; init; } = "";
    public string Subject { get; init; } = "";
}