namespace Nova.Identity.Configuration;

public record EmailVerificationCodeMailOptions : MailOptions
{
    public const string CONFIGKEY = "Nova:Identity:EmailVerificationCode:Mail";
}