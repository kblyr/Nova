using Nova.Configuration;

namespace Nova.Identity.Configuration;

public record UserEmailAddressVerificationMailOptions : MailOptions
{
    public const string CONFIGKEY = "Nova:Identity:UserEmailAddressVerification:Mail";
}