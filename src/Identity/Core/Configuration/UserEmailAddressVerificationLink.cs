namespace Nova.Identity.Configuration;

public record UserEmailAddressVerificationLinkOptions
{
    public const string CONFIGKEY = "Nova:Identity:UserEmailAddressVerification:Link";

    public string BaseUrl { get; init; } = "";
}