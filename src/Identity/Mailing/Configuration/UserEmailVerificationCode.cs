namespace Nova.Identity.Configuration;

public record UserEmailVerificationCodeMailOptions : MailOptions
{
    public const string CONFIGKEY = "Nova:Identity:UserEmailVerificationCode:Mail";
}