namespace Nova.Identity.Configuration;

public record AccessTokenConfig
{
    public TimeSpan Expiration { get; init; }
    public string PublicSigningKeyPath { get; init; } = "";
    public string PrivateSigningKeyPath { get; init; } = "";

    public const string ConfigKey = "AccessToken";
}