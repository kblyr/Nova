namespace Nova;

public record MailConnectionConfig<TPayload>
{
    public string Host { get; init; } = "";
    public int Port { get; init; }
}