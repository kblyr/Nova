namespace Nova.Identity.Configuration;

public record UserEmailAddressVerificationTokenOptions
{
    public const string CONFIGKEY = "Nova:Identity:UserEmailAddressVerification:Token";

    public int Length { get; init; } = 16;
}