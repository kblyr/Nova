namespace Nova.Configuration;

public record EmailVerificationCodeMailOptions : MailOptions
{
    public const string CONFIGKEY = "Nova:Identity:EmailVerificationToken:Mail";
}