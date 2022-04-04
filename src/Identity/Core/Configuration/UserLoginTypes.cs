namespace Nova.Identity.Configuration;

public record UserLoginTypesConfig
{
    public const string CONFIGKEY = "Nova:Identity:UserLoginTypes";

    public short Password { get; init; }
    public short EmailAuth { get; init; }
}