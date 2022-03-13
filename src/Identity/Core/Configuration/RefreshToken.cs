namespace Nova.Identity.Configuration;

public record RefreshTokenConfig
{
    public TimeSpan Expiration { get; init; }

    public const string ConfigKey = "RefreshToken";
}