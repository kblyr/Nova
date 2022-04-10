namespace Nova.Identity.Configuration;

public record UserConfig
{
    public const string CONFIGKEY = "Nova:Identity:User";

    public StringPropertyConfig Username { get; init; } = new() { MinLength = 4, MaxLength = 200 };
    public StringPropertyConfig EmailAddress { get; init; } = new() { MinLength = 4, MaxLength = 200 };
}