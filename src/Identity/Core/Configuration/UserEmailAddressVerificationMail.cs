namespace Nova.Identity.Configuration;

public record UserEmailAddressVerificationMailOptions
{
    public const string CONFIGKEY = "Nova:Identity:UserEmailAddressVerification:Mail";

    public string TemplatePath { get; init; } = "";
    public string Host { get; init; } = "";
    public int Port { get; init; }
    public string SenderAddress { get; init; } = "";
    public string Password { get; init; } = "";
    public string Subject { get; init; } = "";
}