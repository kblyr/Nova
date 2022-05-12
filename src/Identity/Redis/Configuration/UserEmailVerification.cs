namespace Nova.Identity.Configuration;

public record UserEmailVerificationConfig
{
    public const string CONFIGKEY = "Nova:Identity:UserEmailVerification:Redis";

    public TimeSpan Expiration { get; init; }
    public TimeSpan ResendInterval { get; init; }
}